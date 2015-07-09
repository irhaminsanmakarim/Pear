using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.PmsConfigDetails;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.Controllers
{
    public class PmsConfigDetailsController : BaseController
    {
        private readonly IPmsSummaryService _pmsSummaryService;
        private readonly IDropdownService _dropdownService;

        public PmsConfigDetailsController(IPmsSummaryService pmsSummaryService, IDropdownService dropdownService)
        {
            _pmsSummaryService = pmsSummaryService;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            int pmsConfigId = id;
            var viewModel = new CreatePmsConfigDetailsViewModel();
            viewModel.PmsConfigId = pmsConfigId;
            viewModel.Kpis = _dropdownService.GetKpisForPmsConfigDetails(pmsConfigId).MapTo<SelectListItem>();
            viewModel.ScoringTypes = _dropdownService.GetScoringTypes().MapTo<SelectListItem>();
            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreatePmsConfigDetailsViewModel viewModel)
        {
            var request = viewModel.MapTo<CreatePmsConfigDetailsRequest>();
            var response = _pmsSummaryService.CreatePmsConfigDetails(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Details", "PmsSummary", new { id = response.PmsSummaryId });
        }

        public ActionResult Update(int id)
        {
            var response = _pmsSummaryService.GetPmsConfigDetails(id);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<UpdatePmsConfigDetailsViewModel>();
                if (response.ScoreIndicators.Count() == 0)
                {
                    var scoreIndicator = new List<ScoreIndicatorViewModel>();
                    scoreIndicator.Add(new ScoreIndicatorViewModel { Id = 0 });
                    viewModel.ScoreIndicators = scoreIndicator;
                }
                viewModel.ScoringTypes = _dropdownService.GetScoringTypes().MapTo<SelectListItem>();
                return PartialView("_Update", viewModel);
            }

            return base.ErrorPage(response.Message);
        }

        [HttpPost]
        public ActionResult Update(UpdatePmsConfigDetailsViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdatePmsConfigDetailsRequest>();
            var response = _pmsSummaryService.UpdatePmsConfigDetails(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Details", "PmsSummary", new { id = response.PmsSummaryId });
        }

        public ActionResult ScoreIndicatorDetails(int id)
        {
            int pmsConfigDetailsId = id;
            var response = _pmsSummaryService.GetScoreIndicators(new GetScoreIndicatorRequest { PmsConfigDetailId = pmsConfigDetailsId });
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<ScoreIndicatorDetailsViewModel>();
                return PartialView("_ScoreIndicatorDetails", viewModel);
            }

            return base.ErrorPage(response.Message);
        }
    }
}