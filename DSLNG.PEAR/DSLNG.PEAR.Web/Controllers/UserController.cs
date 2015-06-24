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
            var viewModel = new CreateUserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel viewModel)
        {
            var request = viewModel.MapTo<CreateUserRequest>();
            var response = _userService.Create(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }

        public ActionResult Update(int id)
        {
            var response = _userService.GetUser(new GetUserRequest { Id = id });
            var viewModel = response.MapTo<UpdateUserViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateUserViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateUserRequest>();
            var response = _userService.Update(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Update", viewModel);
        }
	}
}