using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.KpiTarget;
using DSLNG.PEAR.Services.Responses.KpiTarget;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity;

namespace DSLNG.PEAR.Services
{
    public class KpiTargetService : BaseService, IKpiTargetService
    {
        public KpiTargetService(IDataContext dataContext)
            : base(dataContext)
        {
        }
        public CreateKpiTargetResponse Create(CreateKpiTargetRequest request)
        {
            var response = new CreateKpiTargetResponse();
            try
            {
                if (request.KpiTargets.Count > 0)
                {
                    foreach (var kpiTarget in request.KpiTargets)
                    {
                        var data = kpiTarget.MapTo<KpiTarget>();
                        data.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == kpiTarget.KpiId);
                        DataContext.KpiTargets.Add(data);
                        DataContext.SaveChanges();
                    }
                    response.IsSuccess = true;
                    response.Message = "KPI Target has been added successfully";
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            return response;
        }


        public GetPmsConfigsResponse GetPmsConfigs(GetPmsConfigsRequest request)
        {
            var pmsSummary = DataContext.PmsSummaries
                                            .Include("PmsConfigs.Pillar")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Measurement")
                                            .FirstOrDefault(x => x.Id == request.Id);
            var response = new GetPmsConfigsResponse();
            var pmsConfigsList = new List<PmsConfig>();
            if (pmsSummary != null)
            {
                var pmsConfigs = pmsSummary.PmsConfigs.ToList();
                if (pmsConfigs.Count > 0)
                {
                    response.PmsConfigs = pmsConfigs.MapTo<GetPmsConfigsResponse.PmsConfig>();
                }
            }

            return response;
        }


        public GetKpiTargetsResponse GetKpiTargets(GetKpiTargetsRequest request)
        {
            var kpis = new List<KpiTarget>();
            if (request.Take != 0)
            {
                kpis = DataContext.KpiTargets.Include(x => x.Kpi).OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take).ToList();
            }
            else
            {
                kpis = DataContext.KpiTargets.Include(x => x.Kpi).ToList();
            }
            var response = new GetKpiTargetsResponse();
            response.KpiTargets = kpis.MapTo<GetKpiTargetsResponse.KpiTarget>();

            return response;
        }

        public GetTargetResponse GetTarget(GetTargetRequest request)
        {
            request = new GetTargetRequest { PeriodeType = PeriodeType.Monthly, PmsSummaryId = 1 };
            var response = new GetTargetResponse();
            try
            {
                var pmsSummary = DataContext.PmsSummaries.Single(x => x.Id == request.PmsSummaryId);

                var pillarsAndKpis = DataContext.PmsConfigDetails
                        .Include(x => x.Kpi)
                        .Include(x => x.Kpi.KpiTargets)
                        .Include(x => x.Kpi.Measurement)
                        .Include(x => x.PmsConfig)
                        .Include(x => x.PmsConfig.PmsSummary)
                        .Include(x => x.PmsConfig.Pillar)
                        .Where(x => x.PmsConfig.PmsSummary.Id == request.PmsSummaryId)
                        .ToList()
                        .GroupBy(x => x.PmsConfig.Pillar)
                        .ToDictionary(x => x.Key);


                foreach (var item in pillarsAndKpis)
                {
                    var pillar = new GetTargetResponse.Pillar();
                    pillar.Id = item.Key.Id;
                    pillar.Name = item.Key.Name;

                    foreach (var val in item.Value)
                    {
                        var targets = new List<GetTargetResponse.KpiTarget>();
                        switch (request.PeriodeType)
                        {
                            case PeriodeType.Monthly:
                                for (int i = 1; i <= 12; i++)
                                {
                                    var kpiTargetsMonthly = val.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly
                                                    && x.Periode.Month == i && x.Periode.Year == pmsSummary.Year);
                                    var kpiTargetMonthly = new GetTargetResponse.KpiTarget();
                                    if (kpiTargetsMonthly == null)
                                    {
                                        kpiTargetMonthly.Id = 0;
                                        kpiTargetMonthly.Periode = new DateTime(pmsSummary.Year, i, 1);
                                        kpiTargetMonthly.Value = null;
                                    }
                                    else
                                    {
                                        kpiTargetMonthly.Id = kpiTargetsMonthly.Id;
                                        kpiTargetMonthly.Periode = kpiTargetsMonthly.Periode;
                                        kpiTargetMonthly.Value = kpiTargetsMonthly.Value;
                                    }

                                    targets.Add(kpiTargetMonthly);
                                }
                                break;
                            case PeriodeType.Yearly:
                                var kpiTargetsYearly =
                                    val.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly
                                                                           && x.Periode.Year == pmsSummary.Year);
                                var kpiTargetYearly = new GetTargetResponse.KpiTarget();
                                if (kpiTargetsYearly == null)
                                {
                                    kpiTargetYearly.Periode = new DateTime(pmsSummary.Year, 1, 1);
                                    kpiTargetYearly.Value = null;
                                }
                                else
                                {
                                    kpiTargetYearly.Periode = kpiTargetsYearly.Periode;
                                    kpiTargetYearly.Value = kpiTargetsYearly.Value;
                                }

                                break;
                        }

                        var kpi = new GetTargetResponse.Kpi
                            {
                                Id = val.Kpi.Id,
                                Measurement = val.Kpi.Measurement.Name,
                                Name = val.Kpi.Name,
                                KpiTargets = targets
                            };

                        pillar.Kpis.Add(kpi);
                    }

                    response.Pillars.Add(pillar);
                }
                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        public UpdateKpiTargetResponse UpdateKpiTarget(UpdateKpiTargetRequest request)
        {
            PeriodeType periodeType = (PeriodeType) Enum.Parse(typeof (PeriodeType), request.PeriodeType);
            var response = new UpdateKpiTargetResponse();
            try
            {
                foreach (var pillar in request.Pillars)
                {
                    foreach (var kpi in pillar.Kpis)
                    {
                        foreach (var kpiTarget in kpi.KpiTargets)
                        {
                            if (kpiTarget.Id == 0)
                            {
                                var kpiTargetNew = new KpiTarget();
                                kpiTargetNew.Value = kpiTarget.Value;
                                kpiTargetNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiTargetNew.PeriodeType = periodeType;
                                kpiTargetNew.Periode = kpiTarget.Periode;
                                kpiTargetNew.IsActive = true;
                                kpiTargetNew.Remark = kpi.Remark;
                                kpiTargetNew.CreatedDate = DateTime.Now;
                                kpiTargetNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiTargets.Add(kpiTargetNew);
                            }
                            else
                            {
                                var kpiTargetNew = new KpiTarget();
                                kpiTargetNew.Id = kpiTarget.Id;
                                kpiTargetNew.Value = kpiTarget.Value;
                                kpiTargetNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiTargetNew.PeriodeType = periodeType;
                                kpiTargetNew.Periode = kpiTarget.Periode;
                                kpiTargetNew.IsActive = true;
                                kpiTargetNew.Remark = kpi.Remark;
                                kpiTargetNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiTargets.Attach(kpiTargetNew);
                                DataContext.Entry(kpiTargetNew).State = EntityState.Modified;
                            }
                        }
                    }
                }
                response.IsSuccess = true;
                response.Message = "KPI Target has been updated successfully";
                DataContext.SaveChanges();
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }
    }
}
