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
        private readonly IDropdownService _dropdownService;

        public CorporatePortofolioController(IPmsSummaryService pmsSummaryService, IDropdownService dropdownService)
        {
            _pmsSummaryService = pmsSummaryService;
            _dropdownService = dropdownService;
        }
        
        

        public ActionResult CreatePmsConfig(int id)
        {
            var viewModel = new CreatePmsConfigViewModel();
            viewModel.PmsSummaryId = id;
            viewModel.Pillars = _dropdownService.GetPillars(id).MapTo<SelectListItem>();
            viewModel.ScoringTypes = _dropdownService.GetScoringTypes().MapTo<SelectListItem>();
            return PartialView("_CreatePmsConfig", viewModel);
        }

        [HttpPost]
        public ActionResult CreatePmsConfig(CreatePmsConfigViewModel viewModel)
        {
            var request = viewModel.MapTo<CreatePmsConfigRequest>();
            var response = _pmsSummaryService.CreatePmsConfig(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
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
                viewModel.PmsSummaryId = id;
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

        public ActionResult CreatePmsConfigDetails(int id)
        {
            var viewModel = new CreatePmsConfigDetailsViewModel();
            viewModel.Kpis = _dropdownService.GetKpis(id).MapTo<SelectListItem>();
            viewModel.ScoringTypes = _dropdownService.GetScoringTypes().MapTo<SelectListItem>();
            return PartialView("_CreatePmsConfigDetails", viewModel);
        }

        
	}
}