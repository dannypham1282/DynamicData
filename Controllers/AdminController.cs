using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    [Authorize(Roles = "SYSADMIN,ADMIN")]
    public class AdminController : Controller
    {
        private readonly IUser _iUser;
        private readonly IUserRoles _iUserRoles;
        private readonly ICommon _iCommon;
        private readonly IOrganization _iOrganization;
        private readonly ILibrary _iLibrary;
        private readonly ILibraryType _iLibraryType;

        public AdminController(IUser iUser, IUserRoles iUserRoles, IOrganization iOrganization, ICommon iCommon, ILibrary iLibrary, ILibraryType iLibraryType)
        {
            _iUser = iUser;
            _iUserRoles = iUserRoles;
            _iOrganization = iOrganization;
            _iCommon = iCommon;
            _iLibrary = iLibrary;
            _iLibraryType = iLibraryType;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users(string guid)
        {
            HttpContext.Session.SetInt32("OrganizationID", Convert.ToInt32(guid));
            if (HttpContext.User.IsInRole("SYSADMIN"))
                ViewData["OrganizationCollection"] = await _iOrganization.OrganizationCollection();
            ViewData["RoleCollection"] = await _iCommon.RoleCollection(HttpContext.User.IsInRole("SYSADMIN"));
            return View();
        }

        public async Task<IActionResult> Organizations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetListStates()
        {
            try
            {
                var optionListStates = new List<DataTableColumnOption>();
                optionListStates.Add(new DataTableColumnOption
                {
                    value = "",
                    label = "--Select--"
                });
                foreach (var state in _iCommon.ListStates())
                {
                    optionListStates.Add(new DataTableColumnOption
                    {
                        value = state.Values.ToArray()[0].ToString(),
                        label = state.Values.ToArray()[2].ToString()
                    });
                }
                return new JsonResult(new { states = optionListStates });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { states = new List<DataTableColumnOption>() });

            }

        }

        //Users CRUD section. also update password and add roles
        [HttpGet]
        [Route("Admin/LoadUsersData")]
        public async Task<IActionResult> LoadUsersData()
        {
            try
            {
                var orgId = HttpContext.User.FindFirstValue("OganizationId");
                if (string.IsNullOrEmpty(orgId))
                    orgId = HttpContext.Session.GetInt32("OrganizationID").ToString();
                List<User> userCollection = await _iUser.UserCollection(HttpContext.User.IsInRole("SYSADMIN"), Convert.ToInt32(orgId));
                return new JsonResult(new
                {
                    data = userCollection
                    .Select(s => new
                    {
                        s.ID,
                        s.Username,
                        s.Firstname,
                        s.Lastname,
                        s.Email,
                        s.Phone,
                        userroles = s.UserRole
                        .Select(f => f.Role).ToList(),
                        userOrganization = s.UserOrganization.Select(o => new { o.Organization.Name, o.Organization.ID }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("Admin/UpdateUserCell")]
        public async Task<IActionResult> UpdateUserCell()
        {
            bool status = false;
            string message = "";
            try
            {
                if (HttpContext.Request.Form["action"].ToString() == "create") //insert
                {
                    User user = new User();
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string fieldName = key.Replace("data[0][", "").Replace("]", "");
                            string keyValue = HttpContext.Request.Form[key].ToString();
                            Common.SetProperty(user, fieldName, keyValue);
                        }
                    }
                    await _iUser.Add(user);//Add User

                    var orgId = HttpContext.User.FindFirstValue("OganizationId");//get organizationID from login claim 
                    if (string.IsNullOrEmpty(orgId)) //If is is null then get it from session. This is when SysAdmin login so organizationID from login claim is null
                        orgId = HttpContext.Session.GetInt32("OrganizationID").ToString();
                    await _iOrganization.AddUserOrganization(user.ID, (Int32)Convert.ToInt32(orgId));

                    status = true;
                    message = "New User " + user.Firstname + " " + user.Lastname + "  has been added";
                }
                else if (HttpContext.Request.Form["action"].ToString() == "edit")//cell update
                {
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string[] keys = Common.getUpdateKey(key);
                            int itemID = Convert.ToInt32(keys[0].ToString());
                            var filedName = keys[1].ToString().Trim();
                            string newValue = HttpContext.Request.Form[key].ToString();
                            var user = await _iUser.FindByID(itemID);
                            Common.SetProperty(user, filedName, newValue);
                            await _iUser.Update(user);
                        }
                    }
                    status = true;
                    message = "Data has has been updated";
                }
                else if (HttpContext.Request.Form["action"].ToString() == "remove")//remove)
                {
                    string[] keys = Common.getUpdateKey(HttpContext.Request.Form.Keys.ToArray()[0]);
                    await _iUser.Delete(Convert.ToInt32(keys[0]));
                    status = true;
                    message = "Record has beeen deleted.";
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
        [Route("Admin/UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles(int userId, string roles)
        {
            bool status = false;
            string message = "";
            try
            {
                await _iUserRoles.Delete(userId);
                if (!string.IsNullOrEmpty(roles))
                {
                    string[] roleId = roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (roleId.Length > 0)
                    {
                        foreach (var id in roleId)
                        {
                            await _iUserRoles.Add(new UserRole { RoleID = Convert.ToInt16(id), UserID = userId });
                        }
                        status = true;
                        message = "User role has been updated";
                    }
                }
                else
                {
                    status = true;
                    message = "User role has been updated";
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
        [Route("Admin/UpdateUserOrganization")]
        public async Task<IActionResult> UpdateUserOrganization(int userId, string orgs)
        {
            bool status = false;
            string message = "";
            try
            {
                await _iOrganization.DeleteUserFromOrganization(userId);
                if (!string.IsNullOrEmpty(orgs))
                {
                    string[] orgId = orgs.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (orgId.Length > 0)
                    {
                        foreach (var id in orgId)
                        {
                            await _iOrganization.AddUserOrganization(userId, Convert.ToInt32(id));
                        }
                        status = true;
                        message = "User Organization has been updated";
                    }
                }
                else
                {
                    status = true;
                    message = "User Organization has been updated";
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
        [Route("Admin/UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(int userId, string password)
        {
            bool status = false;
            string message = "";
            try
            {
                await _iUser.UpdatePassword(userId, password);
                status = true;
                message = "Password has been updated";
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { status = status, message = message });
        }
        //End User CRUD section


        //Organizations CRUD section
        [HttpGet]
        [Route("Admin/LoadOrganizationsData")]
        public async Task<IActionResult> LoadOrganizationsData()
        {
            try
            {
                List<Organization> organizationCollection = await _iOrganization.OrganizationCollection();
                return new JsonResult(new { data = organizationCollection });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("Admin/UpdateOrganizationCell")]
        public async Task<IActionResult> UpdateOrganizationCell() // CRUD operation method
        {
            bool status = false;
            string message = "";
            try
            {
                if (HttpContext.Request.Form["action"].ToString() == "create") //insert
                {
                    Organization organization = new Organization();
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string fieldName = key.Replace("data[0][", "").Replace("]", "");
                            string keyValue = HttpContext.Request.Form[key].ToString();
                            if (fieldName.ToLower() == "states")
                                organization.States = Convert.ToInt32(keyValue);
                            else
                                Common.SetProperty(organization, fieldName, keyValue);
                        }
                    }

                    await _iOrganization.Add(organization);

                    //Add new library to the left side menu when a new Organization is adding to the system
                    var library = new Library();
                    // Find Root Library
                    var rootLibrary = _iLibrary.FindByName("Root");
                    // Find Library Type
                    var libraryType = await _iLibraryType.FindByName("Container");
                    // Set value for Library
                    library.Name = organization.Name;
                    library.ParentID = rootLibrary.Id;
                    library.Title = organization.Name;
                    library.MenuType = "Side Menu";
                    library.LibraryTypeID = libraryType.ID;
                    library.Visible = 1;
                    //add Library to database
                    await _iLibrary.Add(library);
                    //add Organization library to Oranization Library table
                    await _iOrganization.AddOrganizationLibrary(organization.ID, library.ID);
                    status = true;
                    message = "New Organization " + organization.Name + "  has been added";
                }
                else if (HttpContext.Request.Form["action"].ToString() == "edit")//cell update
                {
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        if (key != "action")
                        {
                            string[] keys = Common.getUpdateKey(key);
                            int itemID = Convert.ToInt32(keys[0].ToString());
                            var filedName = keys[1].ToString().Trim();
                            string newValue = HttpContext.Request.Form[key].ToString();
                            var organization = await _iOrganization.FindByID(itemID);
                            if (filedName.ToLower() == "states")
                                organization.States = Convert.ToInt32(newValue);
                            else
                                Common.SetProperty(organization, char.ToUpper(filedName[0]) + filedName.Substring(1), newValue);
                            await _iOrganization.Update(organization);
                        }
                    }
                    status = true;
                    message = "Data has has been updated";
                }
                else if (HttpContext.Request.Form["action"].ToString() == "remove")//remove)
                {
                    string[] keys = Common.getUpdateKey(HttpContext.Request.Form.Keys.ToArray()[0]);
                    await _iOrganization.Delete(Convert.ToInt32(keys[0]));
                    status = true;
                    message = "Record has beeen deleted.";
                }
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return new JsonResult(new { status = status, message = message });
        }
        //End Organization CRUD
    }
}
