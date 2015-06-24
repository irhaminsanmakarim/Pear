using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.ViewModels.User;
using DSLNG.PEAR.Common.Extensions;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

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
            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridUserIndex");
            if (viewModel == null)
                viewModel = CreateGridViewModel();
            return BindingCore(viewModel);
        }

        PartialViewResult BindingCore(GridViewModel gridViewModel)
        {
            gridViewModel.ProcessCustomBinding(
                GetDataRowCount,
                GetData
            );
            return PartialView("_GridViewPartial", gridViewModel);
        }

        static GridViewModel CreateGridViewModel()
        {
            var viewModel = new GridViewModel();
            viewModel.KeyFieldName = "Id";
            viewModel.Columns.Add("Username");
            viewModel.Columns.Add("Email");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridUserIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _userService.GetUsers(new GetUsersRequest()).Users.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _userService.GetUsers(new GetUsersRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Users;
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var response = _userService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
	}
}