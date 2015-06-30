using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Web.ViewModels.KpiTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.KpiTarget;

namespace DSLNG.PEAR.Web.Controllers
{
    public class KpiTargetController : BaseController
    {
        private readonly IKpiTargetService _kpiTargetService;

        public KpiTargetController(IKpiTargetService kpiTargetService)
        {
            _kpiTargetService = kpiTargetService;
        }
        // GET: KpiTarget
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new CreateKpiTargetViewModel();
            viewModel.PillarKpiTarget = FakeListConfigDetail();
            viewModel.PeriodeTypeList.Add(new SelectListItem { Text = "Yearly", Value = "Yearly" });
            //viewModel.PeriodeTypeList.Add(new SelectListItem { Text = "Monthly", Value = "Monthly" });
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateKpiTargetViewModel viewModel)
        {
            if (viewModel.PillarKpiTarget.Count > 0)
            {
                var request = new CreateKpiTargetRequest();
                request.KpiTargets = new List<CreateKpiTargetRequest.KpiTarget>();
                foreach (var item in viewModel.PillarKpiTarget)
                {
                    if (item.KpiTargetList.Count > 0)
                    {
                        foreach (var kpiTargetList in item.KpiTargetList)
                        {
                            kpiTargetList.PeriodeType = (DSLNG.PEAR.Data.Enums.PeriodeType)Enum.Parse(typeof(DSLNG.PEAR.Data.Enums.PeriodeType), viewModel.PeriodeTypeValue);
                            var kpiTarget = kpiTargetList.MapTo<CreateKpiTargetRequest.KpiTarget>();
                            request.KpiTargets.Add(kpiTarget);
                        }
                    }
                }
                var response = _kpiTargetService.Create(request);
                TempData["IsSuccess"] = response.IsSuccess;
                TempData["Message"] = response.Message;
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            viewModel.PillarKpiTarget = FakeListConfigDetail();
            viewModel.PeriodeTypeList.Add(new SelectListItem { Text = "Yearly", Value = "Yearly" });
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
    }
}