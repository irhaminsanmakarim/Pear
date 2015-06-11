using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.ViewModels.User;

namespace DSLNG.PEAR.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var user = _userService.GetUser(new GetUserRequest {Id = 1});

            var userViewModel = new UserViewModel {Email = user.Email, Id = user.Id, Username = user.Username};
            return View(userViewModel);
        }
	}
}