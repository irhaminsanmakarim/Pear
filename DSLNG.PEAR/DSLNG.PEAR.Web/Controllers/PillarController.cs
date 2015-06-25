using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSLNG.PEAR.Services;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Responses.Pillar;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Web.ViewModels.Pillar;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class PillarController : BaseController
    {
        private readonly IPillarService _pillarService;

        public PillarController(IPillarService service)
        {
            _pillarService = service;
        }


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridPillarIndex");
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
            viewModel.Columns.Add("Code");
            viewModel.Columns.Add("Order");
            viewModel.Columns.Add("Color");
            viewModel.Columns.Add("Icon");
            viewModel.Columns.Add("Remark");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridPillarIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _pillarService.GetPillars(new GetPillarsRequest()).Pillars.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _pillarService.GetPillars(new GetPillarsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Pillars;
        }


        public ActionResult Create()
        {
            var viewModel = new CreatePillarViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreatePillarViewModel viewModel)
        {
            var request = viewModel.MapTo<CreatePillarRequest>();
            var response = _pillarService.Create(request);
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
            var response = _pillarService.GetPillar(new GetPillarRequest { Id = id });
            var viewModel = response.MapTo<UpdatePillarViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdatePillarViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdatePillarRequest>();
            var response = _pillarService.Update(request);
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
            var response = _pillarService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}