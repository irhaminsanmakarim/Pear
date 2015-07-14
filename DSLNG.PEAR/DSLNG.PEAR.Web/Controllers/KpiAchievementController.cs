using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.KpiAchievement;
using DSLNG.PEAR.Web.ViewModels.KpiAchievement;

namespace DSLNG.PEAR.Web.Controllers
{
    public class KpiAchievementController : BaseController
    {
        private readonly IKpiAchievementService _kpiAchievementService;
        private readonly IDropdownService _dropdownService;

        public KpiAchievementController(IKpiAchievementService kpiAchievementService, IDropdownService dropdownService)
        {
            _kpiAchievementService = kpiAchievementService;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            var response = _kpiAchievementService.GetAllKpiAchievements();
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<IndexKpiAchievementViewModel>();
                return View(viewModel);    
            }

            return base.ErrorPage(response.Message);
        }

        public ActionResult Update(int id, string periodeType)
        {
            int pmsSummaryId = id;
            PeriodeType pType = string.IsNullOrEmpty(periodeType)
                            ? PeriodeType.Yearly
                            : (PeriodeType)Enum.Parse(typeof (PeriodeType), periodeType);
            var request = new GetKpiAchievementsRequest { PeriodeType = pType, PmsSummaryId = pmsSummaryId };
            var response = _kpiAchievementService.GetKpiAchievements(request);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<UpdateKpiAchievementsViewModel>();
                viewModel.PmsSummaryId = pmsSummaryId;
                viewModel.PeriodeType = pType.ToString();
                viewModel.PeriodeTypes = _dropdownService.GetPeriodeTypesForKpiTargetAndAchievement().MapTo<SelectListItem>();
                return View("Update", viewModel);
            }
            return base.ErrorPage(response.Message);
        }

        [HttpPost]
        public ActionResult Update(UpdateKpiAchievementsViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateKpiAchievementsRequest>();
            var response = _kpiAchievementService.UpdateKpiAchievements(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Update", new { id = viewModel.PmsSummaryId, periodeType = response.PeriodeType.ToString() });
        }

        public ActionResult UpdatePartial(int id, string periodeType)
        {
            int pmsSummaryId = id;
            PeriodeType pType = (PeriodeType)Enum.Parse(typeof(PeriodeType), periodeType);

            var request = new GetKpiAchievementsRequest { PeriodeType = pType, PmsSummaryId = pmsSummaryId };
            var response = _kpiAchievementService.GetKpiAchievements(request);
            string view = pType == PeriodeType.Yearly ? "_yearly" : "_monthly";
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<UpdateKpiAchievementsViewModel>();
                viewModel.PeriodeType = pType.ToString();
                viewModel.PmsSummaryId = pmsSummaryId;
                return PartialView(view, viewModel);
            }

            return Content(response.Message);
        }

        public ActionResult Configuration(int id, string periodeType)
        {
            int roleGroupId = id;
            PeriodeType pType = string.IsNullOrEmpty(periodeType)
                                    ? PeriodeType.Yearly
                                    : (PeriodeType) Enum.Parse(typeof (PeriodeType), periodeType);

            var request = new GetKpiAchievementsConfigurationRequest();
            request.PeriodeType = pType.ToString();
            request.RoleGroupId = roleGroupId;
            var response = _kpiAchievementService.GetKpiAchievementsConfiguration(request);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<ConfigurationKpiAchievementsViewModel>();
                return View(viewModel);    
            }

            return base.ErrorPage(response.Message);

        }

	}
}