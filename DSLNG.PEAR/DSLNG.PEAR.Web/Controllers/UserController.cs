using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.ViewModels.User;
using System.Linq;
using System.Web.Mvc;

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
            /*var user = _userService.GetUser(new GetUserRequest {Id = 1});

            var userViewModel = new UserViewModel {Email = user.Email, Id = user.Id, Username = user.Username};
            return View(userViewModel);*/

            var users = _userService.GetUsers(new GetUsersRequest());

            var viewModel = new UserIndexViewModel() { Users = users.Users.Select(x => new UserViewModel { Email = x.Email, Id = x.Id, Username = x.Username }) };
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        /*DSLNG.PEAR.Data.Persistence.DataContext db = new DSLNG.PEAR.Data.Persistence.DataContext();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.PmsSummaries;
            return PartialView("_GridViewPartial", model.ToList());
        }*/
	}
}