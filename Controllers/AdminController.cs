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
        public AdminController(IUser iUser)
        {
            _iUser = iUser;
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
                return new JsonResult(new { data = userCollection.Select(s => new { s.ID, s.Username, s.Firstname, s.Lastname, s.Email, s.Phone }) });
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
    }
}
