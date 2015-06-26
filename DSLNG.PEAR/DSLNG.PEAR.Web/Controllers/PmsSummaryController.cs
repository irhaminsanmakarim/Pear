using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
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
        private IPmsSummaryService _pmsSummaryService;

        public PmsSummaryController(IPmsSummaryService pmsSummaryService)
        {
            _pmsSummaryService = pmsSummaryService;
        }

        public ActionResult Index()
        {
            var viewModel = new PmsSummaryIndexViewModel();
            var response =
                _pmsSummaryService.GetPmsSummary(new GetPmsSummaryRequest
                    {
                        Month = DateTime.Now.Month,
                        Year = DateTime.Now.Year
                    });

            viewModel.PmsSummaries = response.KpiDatas.MapTo<PmsSummaryViewModel>();
            //viewModel.PmsSummaries = AddFakePmsSummaryData();
            return View(viewModel);
        }

        public ActionResult IndexGridPartial()
        {
            var request = Request.Params;
            var response =
                _pmsSummaryService.GetPmsSummary(new GetPmsSummaryRequest
                    {
                        Month = DateTime.Now.Month,
                        Year = DateTime.Now.Year
                    });
            var viewModel = new PmsSummaryIndexViewModel();
            viewModel.PmsSummaries = response.KpiDatas.MapTo<PmsSummaryViewModel>();
            return PartialView("_IndexGridPartial", viewModel.PmsSummaries);
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
                PerformanceIndicator = "Fatality/Strap Disability",
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
                PerformanceIndicator = "RIF",
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
                PerformanceIndicator = "RIF",
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

        public ActionResult Details()
        {
            //Thread.Sleep(2000);
            return PartialView("_Details");
        }
	}
}