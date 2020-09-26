using DynamicData.Interface;
using DynamicData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    [Authorize(Roles = "SYSADMIN")]
    public class AdminController : Controller
    {
        private readonly IUser _iUser;
        private readonly IUserRoles _iUserRoles;
        private readonly ICommon _iCommon;
        private readonly IOrganization _iOrganization;
        private object optionListStates;

        public AdminController(IUser iUser, IUserRoles iUserRoles, IOrganization iOrganization, ICommon iCommon)
        {
            _iUser = iUser;
            _iUserRoles = iUserRoles;
            _iOrganization = iOrganization;
            _iCommon = iCommon;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Users()
        {
            ViewData["RoleCollection"] = await _iCommon.RoleCollection();
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
                List<User> userCollection = await _iUser.UserCollection();
                return new JsonResult(new { data = userCollection.Select(s => new { s.ID, s.Username, s.Firstname, s.Lastname, s.Email, s.Phone, userroles = s.UserRole.Select(f => f.Role).ToList() }) });
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
                    await _iUser.Add(user);
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
