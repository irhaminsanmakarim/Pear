using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class PmsSummaryController : Controller
    {
        private readonly IPmsSummaryService _pmsSummaryService;

        public PmsSummaryController(IPmsSummaryService pmsSummaryService)
        {
            _pmsSummaryService = pmsSummaryService;
        }

        public ActionResult Index(int? month, int? year)
        {
            var viewModel = new PmsSummaryIndexViewModel();
            var request = new GetPmsSummaryRequest
                {
                    Month = month.HasValue ? month.Value : DateTime.Now.Month,
                    Year = year.HasValue ? year.Value : DateTime.Now.Year
                };

            var response = _pmsSummaryService.GetPmsSummary(request);
            viewModel.PmsSummaries = response.KpiDatas.MapTo<PmsSummaryViewModel>();
            viewModel.Year = request.Year;
            viewModel.Month = request.Month;
            return View(viewModel);
        }

        public ActionResult IndexGridPartial(int? month, int? year)
        {
            var viewModel = new PmsSummaryIndexViewModel();
            var request = new GetPmsSummaryRequest
            {
                Month = month.HasValue ? month.Value : DateTime.Now.Month,
                Year = year.HasValue ? year.Value : DateTime.Now.Year
            };

            var response = _pmsSummaryService.GetPmsSummary(request);
            viewModel.PmsSummaries = response.KpiDatas.MapTo<PmsSummaryViewModel>();
            viewModel.Year = request.Year;
            viewModel.Month = request.Month;
            return PartialView("_IndexGridPartial", viewModel);
        }

        private IEnumerable<PmsSummaryViewModel> AddFakePmsSummaryData()
        {
            IList<PmsSummaryViewModel> list = new List<PmsSummaryViewModel>();
            var pmsSummary1 = new PmsSummaryViewModel
            {
                Id = 1,
                Unit = "Case",
                ActualMonthly = 10,
                ActualYearly = 20,
                ActualYtd = 30,
                Pillar = "Safety",
                Kpi = "Fatality/Strap Disability",
                Weight = 20,
                Score = 35.17,
                TargetMonthly = 10,
                TargetYearly = 29,
                TargetYtd = 20,
            };
            pmsSummary1.Pillar = "<span class='trafficlight grey'></span>Safety (" +
                              pmsSummary1.Weight.ToString() + ")";

            var pmsSummary2 = new PmsSummaryViewModel
            {
                Id = 2,
                Unit = "Case",
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                Pillar = "Safety",
                //OspWeight = 20.00,
                Kpi = "RIF",
                Score = 202.20,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };
            pmsSummary2.Pillar = "<span class='trafficlight grey'></span>Safety (" +
                              pmsSummary2.Weight.ToString() + ")";

            var pmsSummary3 = new PmsSummaryViewModel
            {
                Id = 3,
                Unit = "Case",
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                Kpi = "RIF",
                Score = 202.31,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };
            pmsSummary3.Pillar = "<span class='trafficlight grey'></span>Productivity and Efficiency (" +
                              pmsSummary3.Weight.ToString() + ")";

            list.Add(pmsSummary1);
            list.Add(pmsSummary2);
            list.Add(pmsSummary3);
            return list;

        }

        public ActionResult Details(int id, int month)
        {
            var response = _pmsSummaryService.GetPmsDetails(new GetPmsDetailsRequest() { Id = id, Month = month });
            var viewModel = response.MapTo<PmsDetailsViewModel>();
            var operationDate = new DateTime(response.Year, month, 1);
            viewModel.Title = response.Title;
            viewModel.Year = response.Year;
            viewModel.Month = operationDate.ToString("MMM");
            viewModel.KpiAchievmentMonthly = response.KpiAchievmentMonthly.MapTo<PmsDetailsViewModel.KpiAchievment>();
            viewModel.KpiRelations = response.KpiRelations.MapTo<PmsDetailsViewModel.KpiRelation>();
            return PartialView("_Details", viewModel);
        }
    }
}