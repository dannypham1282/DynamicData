using DynamicData.Data;
using DynamicData.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicData.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        public AccountController(DatabaseContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Account/DoLogin")]
        public async Task<IActionResult> DoLogin(Login login)
        {
            try
            {
                login.Password = HashPassword.DoHash(login.Password.Trim());
                var user = _context.User
                    .Include(u => u.UserRole)
                    .ThenInclude(r => r.Role)
                  .Where(w => w.Username == login.Username && w.Password.Trim() == login.Password).FirstOrDefault();
                if (user == null)
                {
                    ViewData["loginMessage"] = "Invalid Username or Password";
                    return View("Login", "Account");
                }
                else
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    try
                    {
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
                        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Firstname));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, user.Lastname));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Sid, user.ID.ToString()));
                        identity.AddClaim(new Claim("GUID", user.GUID.ToString()));
                        //if (user.Organization != null)
                        //{
                        //    identity.AddClaim(new Claim("OganizationId", user.Organization.ID.ToString()));
                        //    identity.AddClaim(new Claim("Oganization", user.Organization.Name));
                        //}
                    }
                    catch (Exception ex)
                    {

                    }
                    foreach (var ur in user.UserRole)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, ur.Role.Name));
                    }


                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewData["loginMessage"] = ex.Message;
                return View("Login", "Account");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet]
        [Route("Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
