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
    }
}
