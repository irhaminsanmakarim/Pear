using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;

namespace DSLNG.PEAR.Web.Controllers
{
    public class PmsSummaryController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new PmsSummaryIndexViewModel();

            viewModel.PmsSummaries = AddFakePmsSummaryData();
            return View(viewModel);
        }

        public ActionResult IndexGridPartial()
        {
            return PartialView("_IndexGridPartial", AddFakePmsSummaryData());
        }



        private List<PmsSummaryViewModel> AddFakePmsSummaryData()
        {
            var list = new List<PmsSummaryViewModel>();
            var pmsSummary1 = new PmsSummaryViewModel
            {
                Unit = 39,
                Weight = 20,
                ActualMonthly = 10,
                ActualYearly = 20,
                ActualYtd = 30,
                IndexMonthly = 40,
                IndexYearly = 59,
                IndexYtd = 10,
                Osp = "Safety",
                PerformanceIndicator = "Fatality/Strap Disability",
                Score = 35.17,
                TargetMonthly = 10,
                TargetYearly = 29,
                TargetYtd = 20
            };

            var pmsSummary2 = new PmsSummaryViewModel
            {
                Unit = 390,
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                IndexMonthly = 240,
                IndexYearly = 539,
                IndexYtd = 104,
                Osp = "Safety",
                PerformanceIndicator = "RIF",
                Score = 202.20,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };

            var pmsSummary3 = new PmsSummaryViewModel
            {
                Unit = 390,
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                IndexMonthly = 240,
                IndexYearly = 539,
                IndexYtd = 104,
                Osp = "<span class='trafficlight grey'></span>Productivity and Efficiency",
                PerformanceIndicator = "RIF",
                Score = 202.31,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };



            list.Add(pmsSummary1);
            list.Add(pmsSummary2);
            list.Add(pmsSummary3);
            return list;

        }
	}
}