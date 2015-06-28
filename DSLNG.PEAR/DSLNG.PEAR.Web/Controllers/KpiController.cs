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
        private readonly ILevelService _levelService;
        private readonly ITypeService _typeService;
        private readonly IGroupService _groupService;
        private readonly IRoleGroupService _roleGroupService;
        private readonly IMethodService _methodService;
        private readonly IMeasurementService _measurementService;
        private readonly IPillarService _pillarService;

        public KpiController(IKpiService service,
            ILevelService levelService,
            ITypeService typeService,
            IGroupService groupService,
            IRoleGroupService roleGroupService,
            IMethodService methodServie,
            IMeasurementService measurementService,
            IPillarService pillarService)
        {
            _kpiService = service;
            _levelService = levelService;
            _typeService = typeService;
            _groupService = groupService;
            _roleGroupService = roleGroupService;
            _methodService = methodServie;
            _measurementService = measurementService;
            _pillarService = pillarService;
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
            viewModel.KeyFieldName = "Id";
            viewModel.Columns.Add("Code");
            viewModel.Columns.Add("Name");
            viewModel.Columns.Add("PillarId");
            viewModel.Columns.Add("Order");
            viewModel.Columns.Add("IsEconomic");
            viewModel.Columns.Add("Remark");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
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

        public CreateKpiViewModel CreateViewModel(CreateKpiViewModel viewModel)
        {
            viewModel.PillarList = _pillarService.GetPillars(
                new Services.Requests.Pillar.GetPillarsRequest { Skip = 0, Take = 0 }).Pillars.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.LevelList = _levelService.GetLevels(
                new Services.Requests.Level.GetLevelsRequest()).Levels.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.TypeList = _typeService.GetTypes(
                new Services.Requests.Type.GetTypesRequest()).Types.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.GroupList = _groupService.GetGroups(
                new Services.Requests.Group.GetGroupsRequest()).Groups.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.RoleGroupList = _roleGroupService.GetRoleGroups(
                new Services.Requests.RoleGroup.GetRoleGroupsRequest { Skip = 0, Take = 0 }).RoleGroups.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.MethodList = _methodService.GetMethods(
                new Services.Requests.Method.GetMethodsRequest()).Methods.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.MeasurementList = _measurementService.GetMeasurements(
                new Services.Requests.Measurement.GetMeasurementsRequest { Skip = 0, Take = 0 }).Measurements.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            viewModel.KpiList = _kpiService.GetKpis(new GetKpisRequest { Skip = 0, Take = 0 }).Kpis.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            var ytd = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.YtdFormula)).Cast<DSLNG.PEAR.Data.Enums.YtdFormula>();
            var periode = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.PeriodeType)).Cast<DSLNG.PEAR.Data.Enums.PeriodeType>();
            viewModel.YtdFormulaList = ytd.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            viewModel.PeriodeList = periode.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            return viewModel;
        }

        public ActionResult Create()
        {
            var viewModel = new CreateKpiViewModel();
            viewModel = CreateViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateKpiViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Code = viewModel.CodeFromPillar + viewModel.CodeFromLevel + viewModel.Code;
                viewModel.YtdFormula = (DSLNG.PEAR.Web.ViewModels.Kpi.YtdFormula)Enum.Parse(typeof(DSLNG.PEAR.Data.Enums.YtdFormula), viewModel.YtdFormulaValue);
                viewModel.Periode = (DSLNG.PEAR.Web.ViewModels.Kpi.PeriodeType)Enum.Parse(typeof(DSLNG.PEAR.Data.Enums.PeriodeType), viewModel.PeriodeValue);
                var request = viewModel.MapTo<CreateKpiRequest>();
                var response = _kpiService.Create(request);
                TempData["IsSuccess"] = response.IsSuccess;
                TempData["Message"] = response.Message;
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            viewModel = CreateViewModel(viewModel);
            return View("Create", viewModel);
        }

        public ActionResult Update(int id)
        {
            var response = _kpiService.GetKpi(new GetKpiRequest { Id = id });
            var viewModel = response.MapTo<UpdateKpiViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateKpiViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateKpiRequest>();
            var response = _kpiService.Update(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Update", viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var response = _kpiService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }

        public string GetLevelCode(int id)
        {
            var level = _levelService.GetLevel(new Services.Requests.Level.GetLevelRequest { Id = id }).Code;
            return level;
        }

        public string GetPillarCode(int id)
        {
            var pillar = _pillarService.GetPillar(new Services.Requests.Pillar.GetPillarRequest { Id = id }).Code;
            return pillar;
        }
    }
}