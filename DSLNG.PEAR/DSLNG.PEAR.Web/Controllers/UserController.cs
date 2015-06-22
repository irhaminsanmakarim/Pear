using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.ViewModels.User;
using DSLNG.PEAR.Common.Extensions;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var response = _userService.GetUsers(new GetUsersRequest());
            var viewModel = new UserIndexViewModel();
            viewModel.Users = response.Users.MapTo<UserViewModel>();
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }
	}
}