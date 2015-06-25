using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Web.ViewModels.Menu;
using DSLNG.PEAR.Data.Entities;
using DevExpress.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MenuController : Controller
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridMenuIndex");
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
            viewModel.Columns.Add("Order");
            viewModel.Columns.Add("IsRoot");
            viewModel.Columns.Add("Remark");
            viewModel.Columns.Add("Module");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridMenuIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _menuService.GetMenus(new GetMenusRequest()).Menus.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _menuService.GetMenus(new GetMenusRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Menus;
        }

        
        public ActionResult Create()
        {
            var viewModel = new CreateMenuViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateMenuViewModel viewModel)
        {
            var request = viewModel.MapTo<CreateMenuRequest>();
            var response = _menuService.Create(request);
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
            var response = _menuService.GetMenu(new GetMenuRequest { Id = id });
            var viewModel = response.MapTo<UpdateMenuViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateMenuViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateMenuRequest>();
            var response = _menuService.Update(request);
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
            var response = _menuService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
