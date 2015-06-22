using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;
using DevExpress.Web;
using DevExpress.Web.Mvc;

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
            var request = Request.Params;
            return PartialView("_IndexGridPartial", AddFakePmsSummaryData());
        }

       private IEnumerable<PmsSummaryViewModel> AddFakePmsSummaryData()
        {
            IList<PmsSummaryViewModel> list = new List<PmsSummaryViewModel>();
            var pmsSummary1 = new PmsSummaryViewModel
            {
                Id = 1,
                Unit = 39,
                Weight = 20,
                ActualMonthly = 10,
                ActualYearly = 20,
                ActualYtd = 30,
                Osp = "Safety",
                PerformanceIndicator = "Fatality/Strap Disability",
                OspWeight = 20.00,
                Score = 35.17,
                TargetMonthly = 10,
                TargetYearly = 29,
                TargetYtd = 20
            };
            pmsSummary1.Osp = "<span class='trafficlight grey'></span>Safety (" +
                              pmsSummary1.OspWeight.ToString() + ")";

            var pmsSummary2 = new PmsSummaryViewModel
            {
                Id = 2,
                Unit = 390,
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                Osp = "Safety",
                OspWeight = 20.00,
                PerformanceIndicator = "RIF",
                Score = 202.20,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };
            pmsSummary2.Osp = "<span class='trafficlight grey'></span>Safety (" +
                              pmsSummary2.OspWeight.ToString() + ")";

            var pmsSummary3 = new PmsSummaryViewModel
            {
                Id = 3,
                Unit = 390,
                Weight = 100,
                ActualMonthly = 210,
                ActualYearly = 320,
                ActualYtd = 340,
                //Osp = "<span class='trafficlight grey'></span>Productivity and Efficiency",
                OspWeight = 15.00,
                PerformanceIndicator = "RIF",
                Score = 202.31,
                TargetMonthly = 101,
                TargetYearly = 22,
                TargetYtd = 21
            };
            pmsSummary3.Osp = "<span class='trafficlight grey'></span>Productivity and Efficiency (" +
                              pmsSummary3.OspWeight.ToString() + ")";

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