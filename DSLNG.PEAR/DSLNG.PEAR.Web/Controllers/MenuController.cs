﻿using DSLNG.PEAR.Services.Interfaces;
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
using DSLNG.PEAR.Web.Attributes;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Web.DependencyResolution;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MenuController : BaseController
    {

        private readonly IMenuService _menuService;
        private readonly IRoleGroupService _roleService;
        //private readonly IUserService _userService;

        public MenuController(IMenuService menuService, IRoleGroupService roleService)
        {
            _menuService = menuService;
            _roleService = roleService;
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

        [PearMenu]
        public ActionResult SiteMap()
        {
            //get menu active by url request
            //var rootMenuActive = TempData["RootMenuActive"].MapTo<Data.Entities.Menu>();
            var rootMenuActive = TempData["RootMenuActive"].MapTo<Services.Responses.Menu.GetSiteMenuActiveResponse>();
            var userService = ObjectFactory.Container.GetInstance<IUserService>();
            var AuthUser = userService.GetUserByName(new GetUserByNameRequest { Name = HttpContext.User.Identity.Name });
            var menus = new GetSiteMenusResponse();
            if (AuthUser.IsSuccess)
            {
                var role = AuthUser.Role;
                menus = _menuService.GetSiteMenus(new GetSiteMenusRequest() { IncludeChildren = true, RoleId = role.Id });
                menus.RootMenuIdActive = rootMenuActive.Id;
                menus.SelectedMenu = rootMenuActive.SelectedMenu.MapTo<Services.Responses.Menu.GetSiteMenusResponse.Menu>();
            }
            
            

            return PartialView("_SiteMap", menus);
        }

        public CreateMenuViewModel CreateViewModel(CreateMenuViewModel viewModel){
            viewModel.RoleGroupOptions = _roleService.GetRoleGroups(
                new Services.Requests.RoleGroup.GetRoleGroupsRequest { Skip = 0, Take = 0 }).RoleGroups.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            List<SelectListItem> menuList = _menuService.GetMenus(
                new Services.Requests.Menu.GetMenusRequest { Skip = 0, Take = 0 }).Menus.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            //menuList.Insert(0, new SelectListItem { Text = "This is Root", Value = "0", Selected = true });
            viewModel.MenuOptions = menuList;

            return viewModel;
        }
        public ActionResult Create()
        {
            var viewModel = new CreateMenuViewModel();
            viewModel = CreateViewModel(viewModel);

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

        public UpdateMenuViewModel UpdateViewModel(UpdateMenuViewModel viewModel)
        {
            viewModel.RoleGroupOptions = _roleService.GetRoleGroups(
                new Services.Requests.RoleGroup.GetRoleGroupsRequest { Skip = 0, Take = 0 }).RoleGroups.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            List<SelectListItem> menuList = _menuService.GetMenus(
                new Services.Requests.Menu.GetMenusRequest { Skip = 0, Take = 0 }).Menus.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            //menuList.Insert(0, new SelectListItem { Text = "This is Root", Value = "0" });
            viewModel.MenuOptions = menuList;

            return viewModel;
        }

        public ActionResult Update(int id)
        {
            var response = _menuService.GetMenu(new GetMenuRequest { Id = id });
            var viewModel = response.MapTo<UpdateMenuViewModel>();
            viewModel = UpdateViewModel(viewModel);

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
