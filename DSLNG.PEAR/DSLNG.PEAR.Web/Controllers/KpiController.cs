﻿using System;
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
        private readonly IRoleGroupService _roleGroupService;
        private readonly IPillarService _pillarService;
        private readonly IDropdownService _dropdownService;

        public KpiController(IKpiService service,
            ILevelService levelService,
            IRoleGroupService roleGroupService,
            IPillarService pillarService,
            IDropdownService dropdownService)
        {
            _kpiService = service;
            _levelService = levelService;
            _roleGroupService = roleGroupService;
            _pillarService = pillarService;
            _dropdownService = dropdownService;
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
            viewModel.Columns.Add("PillarName");
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
            viewModel.LevelList = _dropdownService.GetLevels().MapTo<SelectListItem>();
            viewModel.PillarList = _dropdownService.GetPillars().MapTo<SelectListItem>();
            viewModel.RoleGroupList = _dropdownService.GetRoleGroups().MapTo<SelectListItem>();
            viewModel.TypeList = _dropdownService.GetTypes().MapTo<SelectListItem>();
            viewModel.GroupList = _dropdownService.GetGroups().MapTo<SelectListItem>();
            viewModel.YtdFormulaList = _dropdownService.GetYtdFormulas().MapTo<SelectListItem>();
            viewModel.PeriodeList = _dropdownService.GetPeriodeTypes().MapTo<SelectListItem>();
            viewModel.MethodList = _dropdownService.GetMethods().MapTo<SelectListItem>();
            viewModel.MeasurementList = _dropdownService.GetMeasurement().MapTo<SelectListItem>();
            viewModel.KpiList = _dropdownService.GetKpis().MapTo<SelectListItem>();
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
                viewModel.Code = string.Format("{0}{1}{2}{3}", viewModel.CodeFromPillar, viewModel.CodeFromLevel, viewModel.Code, viewModel.CodeFromRoleGroup);
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
            viewModel.LevelList = _dropdownService.GetLevels().MapTo<SelectListItem>();
            viewModel.PillarList = _dropdownService.GetPillars().MapTo<SelectListItem>();
            viewModel.RoleGroupList = _dropdownService.GetRoleGroups().MapTo<SelectListItem>();
            viewModel.TypeList = _dropdownService.GetTypes().MapTo<SelectListItem>();
            viewModel.GroupList = _dropdownService.GetGroups().MapTo<SelectListItem>();
            viewModel.YtdFormulaList = _dropdownService.GetYtdFormulas().MapTo<SelectListItem>();
            viewModel.PeriodeList = _dropdownService.GetPeriodeTypes().MapTo<SelectListItem>();
            viewModel.MethodList = _dropdownService.GetMethods().MapTo<SelectListItem>();
            viewModel.MeasurementList = _dropdownService.GetMeasurement().MapTo<SelectListItem>();
            viewModel.KpiList = _dropdownService.GetKpis().MapTo<SelectListItem>();
            viewModel.YtdFormulaList = _dropdownService.GetYtdFormulas().MapTo<SelectListItem>();
            viewModel.PeriodeList = _dropdownService.GetPeriodeTypes().MapTo<SelectListItem>();

            if (!string.IsNullOrEmpty(viewModel.Code))
            {
                var numbers = String.Join("", viewModel.Code.Where(char.IsDigit));
                //var code = 3;
                //var takeCode = viewModel.Code.Length - 3;
                if (viewModel.PillarId.HasValue)
                {
                    viewModel.CodeFromPillar = GetPillarCode(viewModel.PillarId.Value);
                    //code += 2;
                    //takeCode -= viewModel.CodeFromPillar.Length;
                }
                viewModel.CodeFromLevel = GetLevelCode(viewModel.LevelId);
                if (viewModel.RoleGroupId.HasValue)
                {
                    viewModel.CodeFromRoleGroup = !string.IsNullOrEmpty(GetRoleGroupCode(viewModel.RoleGroupId.Value)) ? GetRoleGroupCode(viewModel.RoleGroupId.Value) : "";
                    //takeCode -= viewModel.CodeFromRoleGroup.Length;
                }
                //viewModel.Code = response.Code.Substring(code, takeCode);
                viewModel.Code = numbers;
            }
            
            if (viewModel.RelationModels.Count == 0)
            {
                viewModel.RelationModels.Add(new ViewModels.Kpi.KpiRelationModel { KpiId = 0, Method = "" });
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateKpiViewModel viewModel)
        {
            viewModel.YtdFormula = (DSLNG.PEAR.Web.ViewModels.Kpi.YtdFormula)Enum.Parse(typeof(DSLNG.PEAR.Data.Enums.YtdFormula), viewModel.YtdFormulaValue);
            viewModel.Periode = (DSLNG.PEAR.Web.ViewModels.Kpi.PeriodeType)Enum.Parse(typeof(DSLNG.PEAR.Data.Enums.PeriodeType), viewModel.PeriodeValue);
            viewModel.Code = string.Format("{0}{1}{2}{3}", viewModel.CodeFromPillar, viewModel.CodeFromLevel, viewModel.Code, viewModel.CodeFromRoleGroup);
            var request = viewModel.MapTo<UpdateKpiRequest>();
            var response = _kpiService.Update(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            viewModel.LevelList = _dropdownService.GetLevels().MapTo<SelectListItem>();
            viewModel.PillarList = _dropdownService.GetPillars().MapTo<SelectListItem>();
            viewModel.RoleGroupList = _dropdownService.GetRoleGroups().MapTo<SelectListItem>();
            viewModel.TypeList = _dropdownService.GetTypes().MapTo<SelectListItem>();
            viewModel.GroupList = _dropdownService.GetGroups().MapTo<SelectListItem>();
            viewModel.YtdFormulaList = _dropdownService.GetYtdFormulas().MapTo<SelectListItem>();
            viewModel.PeriodeList = _dropdownService.GetPeriodeTypes().MapTo<SelectListItem>();
            viewModel.MethodList = _dropdownService.GetMethods().MapTo<SelectListItem>();
            viewModel.MeasurementList = _dropdownService.GetMeasurement().MapTo<SelectListItem>();
            viewModel.KpiList = _dropdownService.GetKpis().MapTo<SelectListItem>();
            viewModel.YtdFormulaList = _dropdownService.GetYtdFormulas().MapTo<SelectListItem>();
            viewModel.PeriodeList = _dropdownService.GetPeriodeTypes().MapTo<SelectListItem>();
            if (viewModel.RelationModels.Count == 0)
            {
                viewModel.RelationModels.Add(new ViewModels.Kpi.KpiRelationModel { KpiId = 0, Method = "" });
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

        public string GetRoleGroupCode(int id)
        {
            var roleGroup = _roleGroupService.GetRoleGroup(new Services.Requests.RoleGroup.GetRoleGroupRequest { Id = id }).Code;
            return roleGroup;
        }
    }
}