using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    public class ViewController : Controller
    {
        private readonly ILogger<ViewController> _logger;
        //define Repository Classes
        private readonly ILibrary _iLibrary;
        private readonly ILibraryType _iLibraryType;
        private readonly IUser _iUser;
        private readonly IItem _iItem;
        private readonly IField _iField;
        private readonly IDefaultField _iDefaultField;
        private readonly IFieldValue _iFieldValue;
        private readonly ICommon _iCommon;

        public ViewController(
            ILogger<ViewController> logger,
            ILibrary iLibrary,
            IUser iUser,
            ILibraryType iLibraryType,
            IItem iItem,
            IField iField,
            IDefaultField iDefaultField,
            IFieldValue iFieldValue,
            ICommon iCommon)
        {
            _logger = logger;
            _iLibrary = iLibrary;
            _iLibraryType = iLibraryType;
            _iUser = iUser;
            _iItem = iItem;
            _iField = iField;
            _iDefaultField = iDefaultField;
            _iFieldValue = iFieldValue;
            _iCommon = iCommon;
        }
        public async Task<IActionResult> Index(string guid)
        {
            ViewData["libGuid"] = guid;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BreadCrumbs()
        {
            string requestGuid = Common.GetGuidFromURL(Request.Path.ToString());
            var libraries = await _iLibrary.LibraryCollection();
            return Content(BreadCrumbs(libraries, (requestGuid == "") ? Guid.Empty : Guid.Parse(requestGuid)));
        }

        public string BreadCrumbs(List<Library> libraries, Guid guid)
        {
            StringBuilder sbBC = new StringBuilder();
            var lastNode = libraries.Where(w => w.GUID == guid).FirstOrDefault();
            int counter = 0;
            while (lastNode != null)
            {
                if (lastNode.Title != "Root")
                {
                    if (counter == 0)
                        sbBC.Append("<a  class=\"breadcrumb-item\">" + lastNode.Title + "</a>;");
                    else
                    {
                        sbBC.Append("<a href=\"/" + lastNode.LibraryType.Controller + "/index/" + lastNode.GUID + "\" class=\"breadcrumb-item\">" + lastNode.Title + "</a>;");
                    }
                }
                counter++;
                lastNode = libraries.Where(w => w.ID == lastNode.ParentID).FirstOrDefault();
            }

            string[] breadCrumbs = sbBC.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(breadCrumbs);
            return string.Join("", breadCrumbs);
        }


        [HttpGet]
        public async Task<IActionResult> DataTableColumnDef(string guid)
        {
            List<DataTableColumns> dataTableCol = new List<DataTableColumns>();
            if (guid != "")
            {
                Guid libGuid = Guid.Parse(guid);
                var fields = await _iField.FieldCollection(libGuid);
                DataTableColumns dtCol = new DataTableColumns();
                dtCol.data = "id";
                dtCol.title = "";
                dtCol.className = "sysdelete";
                dtCol.visible = true;
                dtCol.width = 30;
                dtCol.orderable = false;
                dataTableCol.Add(dtCol);

                dtCol = new DataTableColumns();
                dtCol.data = "libraryguid";
                dtCol.title = "";
                dtCol.visible = false;
                dtCol.orderable = false;
                dataTableCol.Add(dtCol);

                foreach (Field field in fields)
                {
                    dtCol = new DataTableColumns();
                    dtCol.data = field.Name.ToLower();
                    dtCol.title = field.Title;
                    dtCol.visible = (field.Visible == 1) ? true : false;
                    dtCol.orderable = true;
                    if (field.FieldType.Type.ToLower() == "currency")
                    {
                        dtCol.render = null;
                        dtCol.className += " classCurrency ";
                    }
                    else if (field.FieldType.Type.ToLower() == "percentage")
                    {
                        dtCol.render = null;
                        dtCol.className += " classPercentage ";
                    }
                    else
                    {
                        dtCol.render = "";
                        dtCol.className = " ";
                    }
                    dtCol.className += "dt_id_" + field.GUID + " ";
                    dtCol.className += (field.Editable == 1) ? "editable" : "noedit";
                    dataTableCol.Add(dtCol);
                }
            }
            return new JsonResult(new
            {
                result = dataTableCol
            });
        }

        public async Task<IActionResult> DataTableColEditor(string guid)
        {
            List<DataTableEditor> dataTableEditor = new List<DataTableEditor>();
            if (guid != "")
            {
                Guid libGuid = Guid.Parse(guid);
                var fields = await _iField.FieldCollection(libGuid);

                DataTableEditor dtCol = new DataTableEditor();
                foreach (Field field in fields)
                {
                    if (field.Visible == 1)
                    {
                        dtCol = new DataTableEditor();

                        dtCol.label = field.Title;
                        dtCol.name = field.Name.ToLower();
                        if (field.Visible == 1)
                        {
                            if (field.FieldType.Type.ToLower() == "datetime")
                            {
                                dtCol.type = "datetime";
                                dtCol.DisplayFormat = "MM/DD/YYYY";
                            }
                            else if (field.FieldType.Type.ToLower() == "dropdown")
                            {
                                dtCol.type = "select";
                                await ColumnOptionsAsync(dtCol, field);
                            }
                            else if (field.FieldType.Type.ToLower() == "radiobutton")
                            {
                                dtCol.type = "radio";
                                await ColumnOptionsAsync(dtCol, field);
                            }
                            else
                                dtCol.type = "text";
                        }
                        else
                        {
                            dtCol.type = "hidden";
                        }
                        if (field.Editable == 0)
                            dtCol.type = "hidden";

                        dataTableEditor.Add(dtCol);
                    }
                }
            }
            return new JsonResult(new { result = dataTableEditor });
        }

        private async Task ColumnOptionsAsync(DataTableEditor dtCol, Field field)
        {
            var optionList = new List<DataTableColumnOption>();
            optionList.Add(new DataTableColumnOption
            {
                value = "",
                label = "--Select--"
            });
            if (field.LookupTable != null) // if there is a lookup table take it first
            {
                var dependentField = await _iField.Find((Guid)field.LookUpId);
                var dependentValue = await _iFieldValue.FindByFieldID(dependentField.ID);
                foreach (var item in dependentValue)
                {
                    optionList.Add(new DataTableColumnOption
                    {
                        value = item.Value.Trim(),
                        label = item.Value.Trim()
                    });
                }
                dtCol.options = optionList;
            }
            else // then check if the manual input for dropdown
            {
                if (!string.IsNullOrEmpty(field.DropdownValue))
                {

                    var dropItems = field.DropdownValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (dropItems.Length > 0)
                    {
                        foreach (string dropItem in dropItems)
                        {
                            optionList.Add(new DataTableColumnOption
                            {
                                value = dropItem.Trim(),
                                label = dropItem.Trim()
                            });
                        }
                        dtCol.options = optionList;
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCell()
        {
            try
            {
                string requestGuid = Common.GetGuidFromURL(Request.Path.ToString());//Get guid from query string
                Guid libraryGuid = (requestGuid == null) ? Guid.Empty : (requestGuid == "") ? Guid.Empty : Guid.Parse(requestGuid);
                if (HttpContext.Request.Form["action"].ToString() == "create") //insert
                {
                    Item item = new Item();
                    item.LibraryGuid = libraryGuid;
                    item.Deleted = 0;
                    await _iItem.Add(item);
                    List<DataTableValidateMessage> validate = new List<DataTableValidateMessage>();
                    List<FieldValue> newRecord = new List<FieldValue>();
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string fieldName = key.Replace("data[0][", "").Replace("]", "");
                            string keyValue = HttpContext.Request.Form[key].ToString();
                            var fieldValue = await _iField.FindByNameAndLibraryGuid(fieldName, libraryGuid);

                            FieldValue value = new FieldValue();
                            //value.Field = fieldValue;
                            value.LibraryGuid = libraryGuid;
                            var validation = ValidateForm(fieldValue, HttpContext.Request.Form[key].ToString());
                            if (validation.Status == null)
                            {
                                if (validate.Count == 0)
                                {
                                    value.Item = item;
                                    value.Value = keyValue;
                                    value.ItemGuid = item.GUID;
                                    value.Created = DateTime.Now;
                                    value.FieldID = fieldValue.ID;
                                    newRecord.Add(value);
                                    //await _iFieldValue.Add(value);
                                }
                            }
                            else
                            {
                                validate.Add(validation);
                            }
                        }
                    }
                    if (validate.Count > 0)
                        return new JsonResult(new { fieldErrors = validate });
                    else
                    {
                        foreach (var newValue in newRecord)
                        {
                            await _iFieldValue.Add(newValue);
                        }
                        return new JsonResult(new { result = "New record has been updated." });
                    }
                }
                else if (HttpContext.Request.Form["action"].ToString() == "edit")//cell update
                {
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string[] keys = Common.getUpdateKey(key);
                            int itemID = Convert.ToInt32(keys[0].ToString());
                            var item = await _iItem.FindByID(itemID);
                            var filedName = keys[1].ToString();
                            var fieldValue = await _iFieldValue.FindbyNameAndLibraryGuidAndItemID(filedName, libraryGuid, itemID);
                            if (fieldValue == null) //propably new field just added to to the library, no field record has been added yet. Add new field to library record
                            {

                                var newField = await _iField.FindByNameAndLibraryGuid(filedName, libraryGuid);
                                fieldValue = new FieldValue();
                                //fieldValue.Field = newField;
                                fieldValue.Value = HttpContext.Request.Form[key].ToString();
                                var validation = ValidateForm(newField, HttpContext.Request.Form[key].ToString());
                                if (validation.Status == null)
                                {
                                    fieldValue.Item = item;
                                    fieldValue.FieldID = newField.ID;
                                    fieldValue.ItemGuid = item.GUID;
                                    fieldValue.LibraryGuid = libraryGuid;
                                    fieldValue.Updated = DateTime.Now;
                                    await _iFieldValue.Add(fieldValue);
                                }
                                else
                                    return new JsonResult(new { status = false, result = validation.Status });
                            }
                            else
                            {
                                var validation = ValidateForm(fieldValue.Field, HttpContext.Request.Form[key].ToString());
                                if (validation.Status == null)
                                {
                                    fieldValue.Value = HttpContext.Request.Form[key].ToString();
                                    fieldValue.Updated = DateTime.Now;
                                    await _iFieldValue.Update(fieldValue);
                                }
                                else
                                    return new JsonResult(new { status = false, result = validation.Status });
                            }
                            return new JsonResult(new { status = true, value = HttpContext.Request.Form[key].ToString(), result = filedName + " value " + fieldValue.Value + " has been updated" });
                        }
                    }
                }
                else if (HttpContext.Request.Form["action"].ToString() == "remove")//remove)
                {
                    string[] keys = Common.getUpdateKey(HttpContext.Request.Form.Keys.ToArray()[0]);
                    await _iItem.Delete(Convert.ToInt32(keys[0]));
                    return new JsonResult(new { result = "Record has been deleted" });
                }
                return new JsonResult(new { result = "success" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { result = ex.Message });
            }
        }

        public async Task<IActionResult> LoadData()
        {
            string requestGuid = Common.GetGuidFromURL(Request.Path.ToString());//Get guid from query string
            Guid libraryGuid = (requestGuid == null) ? Guid.Empty : (requestGuid == "") ? Guid.Empty : Guid.Parse(requestGuid);
            SqlParameter param = new SqlParameter("@LibraryGuid", libraryGuid);
            List<Dictionary<string, object>> dtData = _iCommon.SPReader("spLoadDynamicData '" + libraryGuid + "'");
            if (dtData.Count == 1)
            {
                foreach (KeyValuePair<string, object> data in dtData[0])
                {
                    if (data.Key == "id")
                    {
                        if (data.Value.ToString() == "")
                        {
                            dtData = new List<Dictionary<string, object>>();
                            break;
                        }
                    }

                }
            }
            return new JsonResult(new { data = dtData });
        }

        public DataTableValidateMessage ValidateForm(Field field, string newValue)
        {
            DataTableValidateMessage Validation = new DataTableValidateMessage();
            field.Required = (field.Required == null) ? 0 : field.Required;
            if (field.Editable == 1)
            {
                if (field.Required == 1)
                {
                    if (string.IsNullOrEmpty(newValue))
                    {
                        Validation.Name = field.Name.ToLower();
                        Validation.Status = field.Title + " is required.";
                    }
                }
                if ((field.FieldType.Type.ToString().ToLower() == "currency") || (field.FieldType.Type.ToString().ToLower() == "number") || (field.FieldType.Type.ToString().ToLower() == "percentage"))
                {
                    double tempInt = 0;
                    bool result = double.TryParse(newValue, out tempInt);
                    if (!result)
                    {
                        Validation.Name = field.Name.ToLower();
                        Validation.Status = field.Title + " value " + newValue + " is not a number.";
                    }
                }
            }
            return Validation;
        }

        [HttpGet]
        public async Task<IActionResult> GetFieldsByLibrary(Guid libraryGuid)
        {
            bool status = false;
            try
            {
                var fields = await _iField.FieldCollection(libraryGuid);
                status = true;
                return new JsonResult(new { status = status, result = fields.Select(p => new { p.GUID, p.Title }) });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = status, result = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> InsertLinkLibrary(Guid field, Guid linkLibrary, Guid dependentField, string linkLibraryName)
        {
            bool status = false;
            string message = "";
            try
            {
                var currentField = await _iField.Find(field);
                if (currentField != null)
                {
                    if (linkLibrary != Guid.Empty)
                    {
                        currentField.LookupTable = linkLibrary;
                        currentField.LookUpId = dependentField;
                        await _iField.Update(currentField);
                        status = true;
                        message = currentField.Title + " Has been linked to Library " + linkLibraryName;
                    }
                    else
                    {
                        message = "Link Library cannot be empty";
                        status = false;
                    }
                }
                else
                {
                    status = false;
                    message = "Error adding link librrary";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new
            {
                status = status,
                message = message
            });

        }


        [HttpGet]

        public async Task<IActionResult> DeleteLinkLibrary(Guid fieldGuid)
        {
            bool status = false;
            string message = "";
            try
            {
                var currentField = await _iField.Find(fieldGuid);
                if (currentField != null)
                {
                    currentField.LookupTable = null;
                    currentField.LookUpId = null;
                    await _iField.Update(currentField);
                    status = true;
                    message = "Link Library has been removed";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { status = status, message = message });
        }

        [HttpGet]
        public async Task<IActionResult> ReorderDataTableCol(Guid libraryGuid, string fieldList)
        {
            bool status = false;
            string message = "";
            try
            {
                return new JsonResult(new { status = status, result = "Columns re-order completed" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = status, result = ex.Message });
            }
        }
    }
}
