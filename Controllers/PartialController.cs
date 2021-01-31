using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    public class PartialController : Controller
    {
        private readonly IUser _iUser;
        private readonly IField _iField;
        private readonly IItem _iItem;
        private readonly IFieldValue _iFieldValue;
        private readonly ILibrary _iLibrary;
        private readonly ICommon _iCommon;

        public PartialController(IUser iUser, IField iField, IFieldValue iFieldValue, ILibrary iLibrary, ICommon iCommon,IItem iItem)
        {
            _iUser = iUser;
            _iField = iField;
            _iFieldValue = iFieldValue;
            _iLibrary = iLibrary;
            _iCommon = iCommon;
            _iItem = iItem;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("Partial/_FieldAction")]
        public async Task<IActionResult> _ActionField(string guid, Guid libraryGuid)
        {
            Guid Guid = Guid.Empty;
            if ((guid != "") && (guid != "0"))
                Guid = Guid.Parse(string.IsNullOrEmpty(guid) ? Guid.NewGuid().ToString() : guid);
            try
            {
                var fieldCollection = await _iField.FindByLibraryGuid(libraryGuid);
                if (guid != "0") // load Field object with value for edit form
                {
                    var field = await _iField.FindByGuid(Guid);
                    field.IsEditable = (field.Editable == 1) ? true : false;
                    field.IsVisible = (field.Visible == 1) ? true : false;
                    field.IsRequired = (field.Required == 1) ? true : false;
                    field.IsGrouping = (field.Grouping == 1) ? true : false;
                    field.IsDefaultSort = (field.DefaultSort == 1) ? true : false;
                    field.IsCheckDuplicate = (field.CheckDubplicate == 1) ? true : false;
                    field.FormularLibraryGuid = libraryGuid;
                    field.LibraryCollection = _iCommon.LibraryHierarchy();
                    field.FieldCollection = fieldCollection;
                    return View(field);
                }
                else // load empty Field Object for add form
                {
                    Field field = await _iField.EmptyFieldForView();
                    field.IsEditable = true;
                    field.IsVisible = true;
                    field.IsRequired = false;
                    field.IsGrouping = false;
                    field.IsDefaultSort = false;
                    field.IsCheckDuplicate = false;
                    field.FormularLibraryGuid = libraryGuid;
                    field.LibraryCollection = _iCommon.LibraryHierarchy();
                    field.FieldCollection = fieldCollection;
                    return View(field);
                }
            }
            catch (Exception ex)
            {

            }
            return View(new Field());
        }

        [HttpPost]
        [Route("Partial/_FieldAction/{guid?}")]
        public async Task<IActionResult> _ActionField(Field field)
        {
            bool status = false;
            string message = "";
            Guid libraryGuid = Guid.Empty;
            string queryGuid = Common.GetGuidFromURL(Request.Path.ToString());
            if (!string.IsNullOrEmpty(queryGuid))
                libraryGuid = Guid.Parse(queryGuid);
            if (!await DublicateFieldName(field, libraryGuid))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (field.ID == 0) // Add new field
                        {
                            field.LibraryGuid = libraryGuid;
                            field.GUID = Guid.NewGuid();
                            field.Visible = (field.IsVisible) ? 1 : 0;                         
                            field.Required = (field.IsRequired) ? 1 : 0;
                            field.Grouping = (field.IsGrouping) ? 1 : 0;
                            field.DefaultSort = (field.IsDefaultSort) ? 1 : 0;
                            field.CheckDubplicate = (field.IsCheckDuplicate) ? 1 : 0;
                            field.SortOrder = 100;
                            field.Deleted = 0;
                            if (field.FormularView == "" || field.FormularView == null)
                            {
                                field.Editable = (field.IsEditable) ? 1 : 0;
                            }
                            else
                                field.Editable = 0;

                           await _iField.Add(field);
                            status = true;
                            message = field.Title + " has been added to Library.";
                        }
                        else // update field
                        {
                            field.LibraryGuid = libraryGuid;
                            field.Visible = (field.IsVisible) ? 1 : 0;                           
                            field.Required = (field.IsRequired) ? 1 : 0;
                            field.Grouping = (field.IsGrouping) ? 1 : 0;
                            field.DefaultSort = (field.IsDefaultSort) ? 1 : 0;
                            field.CheckDubplicate = (field.IsCheckDuplicate) ? 1 : 0;
                            field.Deleted = 0;
                            if (field.FormularView == "" || field.FormularView == null)
                            {
                                field.Editable = (field.IsEditable) ? 1 : 0;
                            }
                            else
                                field.Editable = 0;
                            await _iField.Update(field);
                            status = true;
                            message = field.Title + " has been updated.";
                        }
                        // Update Calculate Field value when new Calculate Field is added
                        if (field.FormularView !="")
                        {
                            List<Item> libraryItems = await _iItem.ItemCollection(libraryGuid);
                           foreach (Item item in libraryItems)
                            {
                                await _iFieldValue.CalculateFormularField(item.ID, libraryGuid, field.GUID);
                            }
                        }
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }
            else
            {
                status = false;
                message = field.Name + " already exists in this Library";
            }

            return new JsonResult(new { success = status, message = message });
        }

        private async Task<bool> DublicateFieldName(Field field, Guid libraryGuid)
        {
            var testField = await _iField.FindByNameAndLibraryGuid(field.Name, libraryGuid);

            if (testField != null)
            {
                if (testField.ID == field.ID)
                    return false; // try to update itself it is ok
                else
                    return true;
            }
            else
            {
                return false;
            }

        }

        [HttpGet]

        [Route("Partial/PopulateFieldList")]
        public async Task<IActionResult> PopulateFieldList(Guid libraryGuid)
        {
            try
            {
                var fieldList = await _iField.FieldCollectionForDatatable(libraryGuid);
                return new JsonResult(new { result = fieldList });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLibraryField(string guid, string libraryGuid)
        {
            try
            {
                Guid Guid = Guid.Empty;

                if (!string.IsNullOrEmpty(guid))
                    Guid = Guid.Parse(guid);
                var field = await _iField.Find(Guid);
                await _iFieldValue.DeleteByFieldAndLibrary(field.ID, (!string.IsNullOrEmpty(libraryGuid)) ? Guid.Parse(libraryGuid) : Guid.Empty);
                _iCommon.SPNonQuery("spRemoveLinkLibraryWhenDeleteField '" + field.GUID + "'");
                await _iField.Delete(field);

                //await _iFieldValue.Delete(fieldValue);

                return new JsonResult(new { success = true, message = "Field has been delete." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("Partial/_LinkToLibrary")]
        public async Task<IActionResult> _LinkToLibrary(Guid? fieldGuid, Guid? libraryGuid)
        {
            LinkLibrary linkLibrary = new LinkLibrary();
            List<Field> DependentFields = new List<Field>();
            var library = await _iLibrary.FindByGUID((Guid)libraryGuid);
            var fieldCollection = await _iField.FieldCollection(library.GUID);
            linkLibrary.SetField = (Guid)fieldGuid;
            linkLibrary.Fields = fieldCollection;
            linkLibrary.LinkToLibrary = _iCommon.LibraryHierarchy();
            linkLibrary.DependentFields = DependentFields;
            linkLibrary.LibraryGuid = (Guid)libraryGuid;
            return View(linkLibrary);
        }

        [HttpGet]
        [Route("Partial/LoadLinkLibrary")]
        public async Task<IActionResult> LoadLinkLibrary(Guid libraryGuid)
        {
            try
            {
                var linkLibraries = _iCommon.SPReader("spLoadLinkLibraries '" + libraryGuid + "'");
                return new JsonResult(new { data = linkLibraries });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
