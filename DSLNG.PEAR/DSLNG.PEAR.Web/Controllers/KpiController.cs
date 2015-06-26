using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSLNG.PEAR.Services;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Responses.Kpi;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Web.ViewModels.Kpi;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class KpiController : BaseController
    {
        private readonly IKpiService _kpiService;

        public KpiController(IKpiService service)
        {
            _kpiService = service;
        }


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridKpiIndex");
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
            //viewModel.KeyFieldName = "Id";
            //viewModel.Columns.Add("Code");
            //viewModel.Columns.Add("Name");
            //viewModel.Columns.Add("PillarId");
            //viewModel.Columns.Add("Order");
            //viewModel.Columns.Add("IsEconomic");
            //viewModel.Columns.Add("Remark");
            //viewModel.Columns.Add("IsActive");
            //viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridKpiIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _kpiService.GetKpis(new GetKpisRequest()).Kpis.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _kpiService.GetKpis(new GetKpisRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Kpis;
        }


        public ActionResult Create()
        {
            var viewModel = new CreateKpiViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateKpiViewModel viewModel)
        {
            var request = viewModel.MapTo<CreateKpiRequest>();
            var response = _kpiService.Create(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }

        //public ActionResult Update(int id)
        //{
        //    var response = _pillarService.GetPillar(new GetPillarRequest { Id = id });
        //    var viewModel = response.MapTo<UpdatePillarViewModel>();
        //    return View(viewModel);
        //}

        //[HttpPost]
        //public ActionResult Update(UpdatePillarViewModel viewModel)
        //{
        //    var request = viewModel.MapTo<UpdatePillarRequest>();
        //    var response = _pillarService.Update(request);
        //    TempData["IsSuccess"] = response.IsSuccess;
        //    TempData["Message"] = response.Message;
        //    if (response.IsSuccess)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Update", viewModel);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var response = _pillarService.Delete(id);
        //    TempData["IsSuccess"] = response.IsSuccess;
        //    TempData["Message"] = response.Message;
        //    return RedirectToAction("Index");
        //}
    }
}