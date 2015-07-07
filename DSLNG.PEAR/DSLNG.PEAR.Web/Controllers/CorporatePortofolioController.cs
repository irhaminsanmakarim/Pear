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
using DSLNG.PEAR.Web.ViewModels.PmsConfigDetails;
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

        public ActionResult Details(int id)
        {
            return View("Details");
        }

        /*public ActionResult GetKpis(int id)
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
        }*/

        [HttpPost]
        public ActionResult UpdatePmsConfigDetails(UpdatePmsConfigDetailsViewModel viewModel)
        {
            return Content("in progress");
        }
	}
}