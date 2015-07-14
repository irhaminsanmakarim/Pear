using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.DependencyResolution;
using DSLNG.PEAR.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DSLNG.PEAR.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string message) {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLoginViewModel user) {
            if (ModelState.IsValid)
            {
                if (isValid(user.Username, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index","Home");
                }
                else {
                    ModelState.AddModelError("", "Incorrect Login Data");
                }
                
            }
            else {
                ModelState.AddModelError("", "Incorrect Login Credential");
            }
            return View(user);
        }

        private bool isValid(string username, string password)
        {
            var userService = ObjectFactory.Container.GetInstance<IUserService>();
            var user = userService.Login(new LoginUserRequest { Username = username, Password = password });
            if (user != null) {
                return user.IsSuccess;
            }
            return false;
        }
        public ActionResult Register() {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOff() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        
	}
}