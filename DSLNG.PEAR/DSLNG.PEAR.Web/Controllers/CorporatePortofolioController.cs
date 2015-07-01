using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.CorporatePortofolio;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;

namespace DSLNG.PEAR.Web.Controllers
{
    public class CorporatePortofolioController : BaseController
    {
        private readonly IPmsSummaryService _pmsSummaryService;
        private readonly IPillarService _pillarService;
        private readonly IKpiService _kpiService;

        public CorporatePortofolioController(IPmsSummaryService pmsSummaryService, IPillarService pillarService, IKpiService kpiService)
        {
            _pmsSummaryService = pmsSummaryService;
            _pillarService = pillarService;
            _kpiService = kpiService;
        }

        public ActionResult Index()
        {
            var response = _pmsSummaryService.GetPmsSummaryList(new GetPmsSummaryListRequest());
            if (response.IsSuccess)
            {
                var viewModel = new IndexCorporatePortofolioViewModel();
                viewModel.CorporatePortofolios =
                    response.PmsSummaryList.MapTo<IndexCorporatePortofolioViewModel.CorporatePortofolio>();
                return View(viewModel);    
            }

            return base.ErrorPage(response.Message);
        }

        public ActionResult Details(int id)
        {
            return View("Details");
        }

        public ActionResult PmsSummaryConfiguration(int id)
        {
            var response = _pmsSummaryService.GetPmsSummaryConfiguration(new GetPmsSummaryConfigurationRequest {Id = id});
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<PmsSummaryConfigurationViewModel>();
                return View(viewModel);
            }

            return base.ErrorPage(response.Message);
        }

        public ActionResult ScoreIndicator(int id)
        {
            var response = _pmsSummaryService.GetScoreIndicators(id);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<DialogScoreIndicatorViewModel>();
                return PartialView("_DialogScoreIndicator", viewModel);
            }

            return base.ErrorPage(response.Message);
        }

        public ActionResult PmsConfigDetails(int id)
        {
            var response = _pmsSummaryService.GetPmsConfigDetails(id);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<DialogPmsConfigDetailViewModel>();
                return PartialView("_DialogPmsConfigDetails", viewModel);
            }

            return base.ErrorPage(response.Message);
        }

        public ActionResult GetKpis(int id)
        {
            int pillarId = id;
            var response = _pmsSummaryService.GetKpis(pillarId);
            if (response.IsSuccess)
            {
                var result = (from s in response.Kpis
                              select new
                                  {
                                      id = s.Id,
                                      name = s.Name
                                  }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json("error when load data kpi with pillar id = " + 1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePmsConfigDetails(DialogPmsConfigDetailViewModel viewModel)
        {
            return Content("in progress");
        }

        public ActionResult CreatePmsConfig()
        {
            var viewModel = new CreatePmsConfigViewModel();
            viewModel.Pillars =
                _pillarService.GetPillars(new GetPillarsRequest())
                    .Pillars.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()})
                    .ToList();
            viewModel.ScoringTypes = new List<SelectListItem>()
                {
                    new SelectListItem {Text = ScoringType.Boolean.ToString(), Value = ScoringType.Boolean.ToString()},
                    new SelectListItem {Text = ScoringType.Positive.ToString(), Value = ScoringType.Positive.ToString()},
                    new SelectListItem {Text = ScoringType.Negative.ToString(), Value = ScoringType.Negative.ToString()}
                };
            return PartialView("_CreatePmsConfig", viewModel);
        }

        public ActionResult CreatePmsConfigDetails(int id)
        {
            var viewModel = new CreatePmsConfigDetailsViewModel();
            viewModel.Kpis = _kpiService.GetKpis(new GetKpisRequest{PillarId = id})
                                        .Kpis.Select(
                                            x => new SelectListItem { Text = x.Name.ToString(), Value = x.Id.ToString() })
                                        .ToList();
            viewModel.ScoringTypes = new List<SelectListItem>()
                {
                    new SelectListItem {Text = ScoringType.Boolean.ToString(), Value = ScoringType.Boolean.ToString()},
                    new SelectListItem {Text = ScoringType.Positive.ToString(), Value = ScoringType.Positive.ToString()},
                    new SelectListItem {Text = ScoringType.Negative.ToString(), Value = ScoringType.Negative.ToString()}
                };
            return PartialView("_CreatePmsConfigDetails", viewModel);
        }
	}
}