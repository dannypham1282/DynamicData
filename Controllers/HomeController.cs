using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    //[Authorize(Roles = "User,Admin")]
    //[AllowAnonymous]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //define Repository Classes
        private readonly ILibrary _iLibrary;
        private readonly ILibraryType _iLibraryType;
        private readonly IUser _iUser;
        private readonly IItem _iItem;
        private readonly IField _iField;
        private readonly IDefaultField _iDefaultField;
        private readonly ICommon _iCommon;

        public HomeController(
            ILogger<HomeController> logger,
            ILibrary iLibrary,
            IUser iUser,
            ILibraryType iLibraryType,
            IItem iItem,
            IField iField,
            IDefaultField iDefaultField,
            ICommon iCommon)
        {
            _logger = logger;
            _iLibrary = iLibrary;
            _iLibraryType = iLibraryType;
            _iUser = iUser;
            _iItem = iItem;
            _iField = iField;
            _iDefaultField = iDefaultField;
            _iCommon = iCommon;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/ActionLibrary")]
        public async Task<IActionResult> ActionLibrary(string guid, string parentGuid)
        {
            Guid Guid = Guid.Empty;
            if ((guid != "") && (guid != "0"))
                Guid = Guid.Parse(string.IsNullOrEmpty(guid) ? Guid.NewGuid().ToString() : guid);
            try
            {
                var libraryTypeCollection = await Task.FromResult(_iLibraryType.LibraryTypeCollection().Result);
                var libraryCollection = await _iLibrary.LibraryCollection();

                if (guid != "0")
                {
                    var library = await Task.FromResult(_iLibrary.FindByGUID((Guid)Guid).Result);
                    library.LibraryTypeCollection = libraryTypeCollection;
                    library.LibraryCollection = _iCommon.LibraryHierarchy();
                    return View(library);
                }
                else
                {
                    var library = new Models.Library();
                    int parentLibID = 0;

                    if (_iLibrary.FindByGUID(Guid.Parse(parentGuid)).Result != null)
                        parentLibID = _iLibrary.FindByGUID(Guid.Parse(parentGuid)).Result.ID; //find current Library Parent for the dropdown
                    else
                        parentLibID = _iLibrary.FindByName("Root").Result.ID;
                    library.ID = 0;
                    library.GUID = Guid.NewGuid();
                    library.LibraryTypeCollection = libraryTypeCollection;
                    library.LibraryCollection = _iCommon.LibraryHierarchy();
                    library.ParentID = parentLibID;
                    library.MenuType = "Side Menu";
                    return View(library);
                }
            }
            catch (Exception ex)
            {
                ViewData["loadError"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActionLibrary(Models.Library library)
        {
            bool status = false;
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (library.ID == 0) //Add new
                    {
                        if (!await _iLibrary.CheckDuplicate(library))
                        {
                            library.CreatedDate = DateTime.Now;
                            var user = _iUser.FindByID(Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.Sid))).Result;
                            var libraryType = await _iLibraryType.FindByID((int)library.LibraryTypeID);
                            string testid = HttpContext.User.FindFirst("GUID").ToString();
                            library.CreatedBy = user;
                            library.CreatedDate = DateTime.Now;
                            if (libraryType.Type.ToLower() == "dataview")// 1:Container,2:Data View. If library has type = 2 then create some default fields for the Data view library
                            {
                                var defaultFields = await _iDefaultField.DefaultFieldCollection();
                                foreach (var dfield in defaultFields)
                                {
                                    Field field = new Field();
                                    field.Name = dfield.Name;
                                    field.Title = dfield.Title;
                                    field.FieldType = dfield.FieldType;
                                    if (dfield.Name.ToLower() == "createddate" || (dfield.Name.ToLower() == "createdby"))
                                    {
                                        field.Editable = 0;
                                        field.Visible = 0;
                                    }
                                    else
                                    {
                                        field.Editable = 1;
                                        field.Visible = 1;
                                    }

                                    field.Library = library;
                                    field.LibraryGuid = library.GUID;
                                    await _iField.Add(field);
                                }
                            }
                            await Task.FromResult(_iLibrary.Add(library));
                            status = true;
                            message = "Library " + library.Title + " has been added!";
                        }
                        else
                        {
                            status = false;
                            message = "Library " + library.Title + " already exist!";
                        }
                    }
                    else
                    {
                        if (!await _iLibrary.CheckDuplicate(library))
                        {
                            library.EditedBy = _iUser.FindByID(Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.Sid))).Result;
                            library.EditedDate = DateTime.Now;
                            await _iLibrary.Edit(library);

                            status = true;
                            message = "Library " + library.Title + " has been updated!";
                        }
                        else
                        {
                            status = false;
                            message = "Library " + library.Title + " already exist!";
                        }
                    }
                }
                else
                {
                    message = "Model State is not valid. ";
                    message += string.Join("; ", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { success = status, message = message });
        }

        public async Task<IActionResult> DeleteLibrary(string guid)
        {
            bool status = false;
            string message = "";
            Guid Guid = Guid.Empty;
            try
            {
                if (guid != null)
                    Guid = Guid.Parse(guid);
                string libName = _iLibrary.FindByGUID(Guid).Result.Title;
                //Remove all Fields related to labrary
                await _iField.DeleteByLibrary(Guid);
                //Remove Library
                await _iLibrary.Delete(Guid);
                status = true;
                message = libName + " has been deleted";
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { success = status, message = message });
        }

        [HttpGet]
        public IActionResult _SideMenu(string guid)
        {
            string requestGuid = Common.GetGuidFromURL(Request.Path.ToString());//Get guid from query string
            var navLib = _iLibrary.LibraryCollectionForSideMenu().Result;
            int rootID = navLib.Where(x => x.ParentID == null).FirstOrDefault().ID;
            Guid currentOpenGuid = Guid.Empty;
            if (guid == null)
                guid = requestGuid; // if guid pass from view is null then use the one in the url
            try
            {
                currentOpenGuid = (guid == null) ? Guid.Empty : (guid == "") ? Guid.Empty : Guid.Parse(guid);
            }
            catch (Exception ex)
            {
                currentOpenGuid = Guid.Empty;
            }
            string parentNodes = FindAllParents(navLib, currentOpenGuid);
            return Content(BuildSideMenu(navLib, rootID, parentNodes, requestGuid));
        }

        public string BuildSideMenu(List<Models.Library> libraries, int? parentID, string parentNodes, string currentNode)
        {
            StringBuilder sbSideMenu = new StringBuilder();
            var parent = libraries.Where(w => w.ParentID == parentID).OrderBy(o => o.Title).ToList();
            foreach (var item in parent)
            {
                var child = libraries.Where(w => w.ParentID == item.ID).OrderBy(o => o.SortOrder).ToList();

                if (child.Count > 0)
                {
                    if (parentNodes.IndexOf(item.GUID.ToString()) > -1)
                        sbSideMenu.Append("<li class=\"nav-item has-treeview menu-open\" id=\"" + item.GUID + "\">");
                    else
                    {
                        sbSideMenu.Append("<li class=\"nav-item has-treeview\" id=\"" + item.GUID + "\">");
                    }
                }
                else
                {
                    if (parentNodes.IndexOf(item.GUID.ToString()) > -1)
                        sbSideMenu.Append("<li class=\"nav-item menu open\" id=\"" + item.GUID + "\">");
                    else
                    {
                        if (currentNode.IndexOf(item.GUID.ToString()) > -1)
                            sbSideMenu.Append("<li class=\"nav-item selectedmenu\" id=\"" + item.GUID + "\">");
                        else
                            sbSideMenu.Append("<li class=\"nav-item\" id=\"" + item.GUID + "\">");
                    }
                }
                sbSideMenu.Append("<a href = \"/" + item.LibraryType.Controller + "/index/" + item.GUID + "\" class=\"nav-link\">");
                sbSideMenu.Append("<i class=\"nav-icon fas " + item.LibraryType.Icon + "\"></i>");
                sbSideMenu.Append("<p>");
                sbSideMenu.Append(item.Title);
                if (child.Count > 0)
                    sbSideMenu.Append("<i class=\"right fas fa-angle-left\"></i>");
                sbSideMenu.Append("</p>");
                sbSideMenu.Append("</a>");
                if (child.Count > 0)
                    sbSideMenu.Append(SideSubMenu(libraries, item.ID, parentNodes, currentNode));
                sbSideMenu.Append("</li>");
            }

            return sbSideMenu.ToString();
        }

        private string SideSubMenu(List<Models.Library> libraries, int? parentID, string parentNodes, string currentNode)
        {
            StringBuilder sbSubMenu = new StringBuilder();
            var parent = libraries.Where(x => x.ParentID == parentID).OrderBy(o => o.SortOrder).ToList();
            if (parent.Count > 0)
            {
                sbSubMenu.Append("<ul class=\"nav nav-treeview\">");
                foreach (var item in parent)
                {
                    var child = libraries.Where(w => w.ParentID == item.ID).OrderBy(o => o.SortOrder).ToList();
                    if (child.Count > 0)
                    {
                        if (parentNodes.IndexOf(item.GUID.ToString()) > -1)
                            sbSubMenu.Append("<li class=\"nav-item has-treeview menu-open\" id=\"" + item.GUID + "\">");
                        else
                            sbSubMenu.Append("<li class=\"nav-item has-treeview\" id=\"" + item.GUID + "\">");
                    }
                    else
                    {
                        if (parentNodes.IndexOf(item.GUID.ToString()) > -1)
                        {
                            if (currentNode.IndexOf(item.GUID.ToString()) > -1)
                                sbSubMenu.Append("<li class=\"nav-item menu-open selectedmenu\" id=\"" + item.GUID + "\">");
                            else
                                sbSubMenu.Append("<li class=\"nav-item menu-open\" id=\"" + item.GUID + "\">");
                        }
                        else
                        {
                            if (currentNode.IndexOf(item.GUID.ToString()) > -1)
                                sbSubMenu.Append("<li class=\"nav-item selectedmenu\" id=\"" + item.GUID + "\">");
                            else
                                sbSubMenu.Append("<li class=\"nav-item\" id=\"" + item.GUID + "\">");
                        }
                    }

                    sbSubMenu.Append("<a href = \"/" + item.LibraryType.Controller + "/index/" + item.GUID + "\" class=\"nav-link\">");
                    sbSubMenu.Append("<i class=\"nav-icon fas " + item.LibraryType.Icon + "\"></i>");
                    sbSubMenu.Append("<p>");
                    sbSubMenu.Append(item.Title);
                    if (child.Count > 0)
                        sbSubMenu.Append("<i class=\"right fas fa-angle-left\"></i>");
                    sbSubMenu.Append("</p>");
                    sbSubMenu.Append("</a>");
                    if (child.Count > 0)
                        sbSubMenu.Append(SideSubMenu(libraries, item.ID, parentNodes, currentNode));
                    sbSubMenu.Append("</li>");
                }
                sbSubMenu.Append("</ul>");
            }
            return sbSubMenu.ToString();
        }

        public string FindAllParents(List<Library> libraries, Guid guid)
        {
            StringBuilder sbParents = new StringBuilder();
            var lastNode = libraries.Where(w => w.GUID == guid).FirstOrDefault();
            while (lastNode != null)
            {
                sbParents.Append(lastNode.GUID + ";");
                lastNode = libraries.Where(w => w.ID == lastNode.ParentID).FirstOrDefault();
            }
            return sbParents.ToString(); ;
        }

        public async Task<IActionResult> SaveLibraryOrder(string[] ids)
        {
            bool status = false;
            string message = "";
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    await _iLibrary.UpdateLibraryOrder(Guid.Parse(ids[i]), i);
                }
                status = true;
                message = "Sort complete";
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { success = status, message = message });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
