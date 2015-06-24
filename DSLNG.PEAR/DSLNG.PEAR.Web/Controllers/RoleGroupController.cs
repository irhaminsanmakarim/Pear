using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.RoleGroup;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using DevExpress.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class RoleGroupController : BaseController
    {
        private readonly IRoleGroupService _roleGroupService;

        public RoleGroupController(IRoleGroupService service)
        {
            _roleGroupService = service;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridTypeIndex");
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
            viewModel.Columns.Add("Name");
            viewModel.Columns.Add("Icon");
            viewModel.Columns.Add("Remark");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridRoleGroupIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _roleGroupService.GetRoleGroups(new GetRoleGroupsRequest()).RoleGroups.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _roleGroupService.GetRoleGroups(new GetRoleGroupsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).RoleGroups;
        }

        public ActionResult Create()
        {
            var viewModel = new CreateRoleGroupViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateRoleGroupViewModel viewModel)
        {
            var request = viewModel.MapTo<CreateRoleGroupRequest>();
            var response = _roleGroupService.Create(request);
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
            var response = _roleGroupService.GetRoleGroup(new GetRoleGroupRequest { Id = id });
            var viewModel = response.MapTo<UpdateRoleGroupViewModel>();
            return View(viewModel);
        }

        
        [HttpPost]
        public ActionResult Update(UpdateRoleGroupViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateRoleGroupRequest>();
            var response = _roleGroupService.Update(request);
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
            var response = _roleGroupService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}