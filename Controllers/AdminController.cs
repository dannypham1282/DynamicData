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
        public AdminController(IUser iUser, IUserRoles iUserRoles)
        {
            _iUser = iUser;
            _iUserRoles = iUserRoles;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            return View();
        }


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
            string messsage = "";
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
                    messsage = "New User " + user.Firstname + " " + user.Lastname + "  has been added";
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
                    messsage = "Data has has been updated";
                }
                else if (HttpContext.Request.Form["action"].ToString() == "remove")//remove)
                {
                    string[] keys = Common.getUpdateKey(HttpContext.Request.Form.Keys.ToArray()[0]);
                    await _iUser.Delete(Convert.ToInt32(keys[0]));
                    status = true;
                    messsage = "Record has beeen deleted.";
                }
            }
            catch (Exception ex)
            {
                status = false;
                messsage = ex.Message;
            }
            return new JsonResult(new { status = status, message = messsage });
        }

        [HttpGet]
        [Route("Admin/UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles(int[] roleId, int userId)
        {
            bool status = false;
            string messsage = "";
            try
            {
                await _iUserRoles.Delete(userId);
                if (roleId.Length > 0)
                {
                    foreach (int id in roleId)
                    {

                        await _iUserRoles.Add(new UserRole { RoleID = id, UserID = userId });
                    }
                    status = true;
                    messsage = "User role has been updated";
                }
            }
            catch (Exception ex)
            {
                status = false;
                messsage = ex.Message;
            }
            return new JsonResult(new { status = status, messsage = messsage });
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
    }
}
