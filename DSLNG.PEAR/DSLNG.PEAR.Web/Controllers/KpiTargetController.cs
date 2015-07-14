using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Responses.KpiTarget;
using DSLNG.PEAR.Web.ViewModels.KpiTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.KpiTarget;
using DevExpress.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class KpiTargetController : BaseController
    {
        private readonly IKpiTargetService _kpiTargetService;
        private readonly IDropdownService _dropdownService;

        public KpiTargetController(IKpiTargetService kpiTargetService, IDropdownService dropdownService)
        {
            _kpiTargetService = kpiTargetService;
            _dropdownService = dropdownService;
        }

        public ActionResult Update(int id, string periodeType)
        {
            int pmsSummaryId = id;
            PeriodeType pType = string.IsNullOrEmpty(periodeType)
                            ? PeriodeType.Yearly
                            : (PeriodeType)Enum.Parse(typeof(PeriodeType), periodeType);
            var request = new GetKpiTargetRequest { PeriodeType = pType, PmsSummaryId = pmsSummaryId };
            var response = _kpiTargetService.GetKpiTarget(request);
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<UpdateKpiTargetViewModel>();
                viewModel.PmsSummaryId = pmsSummaryId;
                viewModel.PeriodeType = pType.ToString();
                viewModel.PeriodeTypes = _dropdownService.GetPeriodeTypesForKpiTargetAndAchievement().MapTo<SelectListItem>();
                return View("Update", viewModel);
            }
            return base.ErrorPage(response.Message);
        }

        [HttpPost]
        public ActionResult Update(UpdateKpiTargetViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateKpiTargetRequest>();
            var response = _kpiTargetService.UpdateKpiTarget(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Update", new { id = viewModel.PmsSummaryId, periodeType = response.PeriodeType.ToString() });
        }

        public ActionResult UpdatePartial(int id, string periodeType)
        {
            int pmsSummaryId = id;
            PeriodeType pType = (PeriodeType)Enum.Parse(typeof(PeriodeType), periodeType);

            var request = new GetKpiTargetRequest { PeriodeType = pType, PmsSummaryId = pmsSummaryId };
            var response = _kpiTargetService.GetKpiTarget(request);
            string view = pType == PeriodeType.Yearly ? "_yearly" : "_monthly";
            if (response.IsSuccess)
            {
                var viewModel = response.MapTo<UpdateKpiTargetViewModel>();
                viewModel.PeriodeType = pType.ToString();
                viewModel.PmsSummaryId = pmsSummaryId;
                return PartialView(view, viewModel);
            }

            return Content(response.Message);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridKpiTargetIndex");
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
            viewModel.Columns.Add("KpiName");
            viewModel.Columns.Add("PeriodeType");
            viewModel.Columns.Add("Value");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridKpiTargetIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _kpiTargetService.GetKpiTargets(new GetKpiTargetsRequest { Take = 0, Skip = 0 }).KpiTargets.Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _kpiTargetService.GetKpiTargets(new GetKpiTargetsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).KpiTargets;
        }

        public ActionResult Create()
        {
            var viewModel = new CreateKpiTargetViewModel();
            viewModel = SetViewModel(viewModel);
            return View(viewModel);
        }

        public CreateKpiTargetViewModel SetViewModel(CreateKpiTargetViewModel viewModel)
        {
            var pmsConfigs = _kpiTargetService.GetPmsConfigs(new GetPmsConfigsRequest { Id = 1 }).PmsConfigs;
            if (pmsConfigs.Count > 0)
            {
                foreach (var pmsConfig in pmsConfigs)
                {
                    var pillarSelectListItem = new List<SelectListItem>();
                    pillarSelectListItem.Add(new SelectListItem { Text = pmsConfig.Pillar.Name, Value = pmsConfig.Pillar.Id.ToString() });
                    var pmsConfigDetails = pmsConfig.PmsConfigDetailsList;
                    if (pmsConfigDetails.Count > 0)
                    {
                        var kpiTargetList = new List<KpiTarget>();
                        foreach (var pmsConfigDetail in pmsConfigDetails)
                        {
                            var kpiSelectListItem = new List<SelectListItem>();
                            kpiSelectListItem.Add(new SelectListItem { Text = pmsConfigDetail.Kpi.Name, Value = pmsConfigDetail.Kpi.Id.ToString() });
                            var kpi = pmsConfigDetail.Kpi.MapTo<Kpi>();
                            kpiTargetList.Add(new KpiTarget
                            {
                                Kpi = kpi,
                                KpiList = kpiSelectListItem,
                                Periode = new DateTime(pmsConfig.PmsSummary.Year, 1, 1),
                                KpiId = pmsConfigDetail.Kpi.Id
                                //IsActive = pmsConfig.IsActive 
                            });
                        }
                        viewModel.PillarKpiTarget.Add(new PillarTarget
                        {
                            PillarList = pillarSelectListItem,
                            KpiTargetList = kpiTargetList
                        });
                    }
                }
            }
            return viewModel;
        }

        [HttpPost]
        public ActionResult Create(CreateKpiTargetViewModel viewModel)
        {
            if (viewModel.PillarKpiTarget.Count > 0)
            {
                var request = new CreateKpiTargetsRequest();
                request.KpiTargets = new List<CreateKpiTargetsRequest.KpiTarget>();
                foreach (var item in viewModel.PillarKpiTarget)
                {
                    if (item.KpiTargetList.Count > 0)
                    {
                        foreach (var kpi in item.KpiTargetList)
                        {
                            request.KpiTargets.Add(new CreateKpiTargetsRequest.KpiTarget
                            {
                                IsActive = true,
                                KpiId = kpi.KpiId,
                                Periode = kpi.Periode,
                                PeriodeType = (DSLNG.PEAR.Data.Enums.PeriodeType)kpi.PeriodeType,
                                Remark = kpi.Remark,
                                Value = kpi.Value
                            });
                        }
                    }
                }
                //foreach (var item in viewModel.PillarKpiTarget)
                //{
                //    if (item.KpiTargetList.Count > 0)
                //    {
                //        foreach (var kpiTargetList in item.KpiTargetList)
                //        {
                //            if (kpiTargetList.ValueList.Count > 0)
                //            {
                //                for (int i = 0; i < kpiTargetList.ValueList.Count; i++)
                //                {
                //                    if (i == 0)
                //                    {
                //                        kpiTargetList.PeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType.Yearly;
                //                        kpiTargetList.Value = kpiTargetList.ValueList[0];
                //                        var kpiTarget = kpiTargetList.MapTo<CreateKpiTargetRequest.KpiTarget>();
                //                        request.KpiTargets.Add(kpiTarget);
                //                    }
                //                    else
                //                    {
                //                        kpiTargetList.PeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType.Monthly;
                //                        kpiTargetList.Periode = new DateTime(kpiTargetList.Periode.Year, i, 1);
                //                        kpiTargetList.Value = kpiTargetList.ValueList[i];
                //                        var kpiTarget = kpiTargetList.MapTo<CreateKpiTargetRequest.KpiTarget>();
                //                        request.KpiTargets.Add(kpiTarget);
                //                    }

                //                }
                //            }
                //        }
                //    }
                //}
                var response = _kpiTargetService.Creates(request);
                TempData["IsSuccess"] = response.IsSuccess;
                TempData["Message"] = response.Message;
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            viewModel = SetViewModel(viewModel);
            return View(viewModel);
        }

        public List<PillarTarget> FakeListConfigDetail()
        {
            var data = new List<PillarTarget>();
            var pillars = new List<Pillar>();
            pillars.Add(new Pillar { Id = 1, Name = "Safety" });
            pillars.Add(new Pillar { Id = 2, Name = "Pillar 2" });
            if (pillars.Count > 0)
            {
                foreach (var pillar in pillars)
                {
                    //get list pillar from pmsConfig
                    var pillarSelectListItem = new List<SelectListItem>();
                    pillarSelectListItem.Add(new SelectListItem { Text = pillar.Name, Value = pillar.Id.ToString() });

                    //get list kpi by pillarId
                    var kpiList = new List<Kpi>();
                    kpiList.Add(new Kpi
                    {
                        Pillar = pillar,
                        Name = "Safety Incident",
                        Id = 1,
                        Unit = "Case"
                    });
                    kpiList.Add(new Kpi
                    {
                        Pillar = pillar,
                        Name = "Fatality",
                        Id = 1,
                        Unit = "Case"
                    });
                    var kpiTargetList = new List<KpiTarget>();
                    if (kpiList.Count > 0)
                    {
                        foreach (var kpi in kpiList)
                        {
                            var kpiSelectListItem = new List<SelectListItem>();
                            kpiSelectListItem.Add(new SelectListItem { Text = kpi.Name, Value = kpi.Id.ToString() });
                            kpiTargetList.Add(new KpiTarget { Kpi = kpi, KpiList = kpiSelectListItem });
                        }
                    }

                    data.Add(new PillarTarget
                    {
                        PillarList = pillarSelectListItem,
                        KpiTargetList = kpiTargetList
                    });
                }
            }
            return data;
        }

        [HttpPost]
        public JsonResult KpiTargetItem(KpiTargetItem kpiTarget)
        {
            if (kpiTarget.Id > 0)
            {
                var request = kpiTarget.MapTo<UpdateKpiTargetItemRequest>();
                var response = _kpiTargetService.UpdateKpiTargetItem(request);
                return Json(new { Id = response.Id, Message = response.Message });
            }
            else
            {
                var request = kpiTarget.MapTo<CreateKpiTargetRequest>();
                var response = _kpiTargetService.Create(request);
                return Json(new { Id = response.Id, Message = response.Message });
            }
        }
    }
}