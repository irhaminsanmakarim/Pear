using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.KpiAchievement;
using DSLNG.PEAR.Services.Responses.KpiAchievement;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class KpiAchievementService : BaseService, IKpiAchievementService
    {
        public KpiAchievementService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetKpiAchievementsResponse GetKpiAchievements(GetKpiAchievementsRequest request)
        {
            var response = new GetKpiAchievementsResponse();
            try
            {
                var pmsSummary = DataContext.PmsSummaries.Single(x => x.Id == request.PmsSummaryId);

                var pillarsAndKpis = DataContext.PmsConfigDetails
                        .Include(x => x.Kpi)
                        .Include(x => x.Kpi.KpiAchievements)
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
                    var pillar = new GetKpiAchievementsResponse.Pillar();
                    pillar.Id = item.Key.Id;
                    pillar.Name = item.Key.Name;

                    foreach (var val in item.Value)
                    {
                        var achievements = new List<GetKpiAchievementsResponse.KpiAchievement>();
                        switch (request.PeriodeType)
                        {
                            case PeriodeType.Monthly:
                                for (int i = 1; i <= 12; i++)
                                {
                                    var kpiAchievementsMonthly = val.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly
                                                    && x.Periode.Month == i && x.Periode.Year == pmsSummary.Year);
                                    var kpiAchievementMonthly = new GetKpiAchievementsResponse.KpiAchievement();
                                    if (kpiAchievementsMonthly == null)
                                    {
                                        kpiAchievementMonthly.Id = 0;
                                        kpiAchievementMonthly.Periode = new DateTime(pmsSummary.Year, i, 1);
                                        kpiAchievementMonthly.Value = null;
                                        kpiAchievementMonthly.Remark = null;
                                    }
                                    else
                                    {
                                        kpiAchievementMonthly.Id = kpiAchievementsMonthly.Id;
                                        kpiAchievementMonthly.Periode = kpiAchievementsMonthly.Periode;
                                        kpiAchievementMonthly.Value = kpiAchievementsMonthly.Value;
                                        kpiAchievementMonthly.Remark = kpiAchievementsMonthly.Remark;
                                    }

                                    achievements.Add(kpiAchievementMonthly);
                                }
                                break;
                            case PeriodeType.Yearly:
                                var kpiAchievementsYearly =
                                    val.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly
                                                                           && x.Periode.Year == pmsSummary.Year);
                                var kpiAchievementYearly = new GetKpiAchievementsResponse.KpiAchievement();
                                if (kpiAchievementsYearly == null)
                                {
                                    kpiAchievementYearly.Id = 0;
                                    kpiAchievementYearly.Periode = new DateTime(pmsSummary.Year, 1, 1);
                                    kpiAchievementYearly.Value = null;
                                    kpiAchievementYearly.Remark = null;
                                }
                                else
                                {
                                    kpiAchievementYearly.Id = kpiAchievementsYearly.Id;
                                    kpiAchievementYearly.Periode = kpiAchievementsYearly.Periode;
                                    kpiAchievementYearly.Value = kpiAchievementsYearly.Value;
                                    kpiAchievementYearly.Remark = kpiAchievementsYearly.Remark;
                                }
                                achievements.Add(kpiAchievementYearly);

                                break;
                        }

                        var kpi = new GetKpiAchievementsResponse.Kpi
                        {
                            Id = val.Kpi.Id,
                            Measurement = val.Kpi.Measurement.Name,
                            Name = val.Kpi.Name,
                            KpiAchievements = achievements
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

        public UpdateKpiAchievementsResponse UpdateKpiAchievements(UpdateKpiAchievementsRequest request)
        {
            PeriodeType periodeType = (PeriodeType)Enum.Parse(typeof(PeriodeType), request.PeriodeType);
            var response = new UpdateKpiAchievementsResponse();
            response.PeriodeType = periodeType;
            
            try
            {
                foreach (var pillar in request.Pillars)
                {
                    foreach (var kpi in pillar.Kpis)
                    {
                        foreach (var kpiAchievement in kpi.KpiAchievements)
                        {
                            if (kpiAchievement.Id == 0)
                            {
                                var kpiAchievementNew = new KpiAchievement();
                                kpiAchievementNew.Value = kpiAchievement.Value;
                                kpiAchievementNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiAchievementNew.PeriodeType = periodeType;
                                kpiAchievementNew.Periode = kpiAchievement.Periode;
                                kpiAchievementNew.IsActive = true;
                                kpiAchievementNew.Remark = kpiAchievement.Remark;
                                kpiAchievementNew.CreatedDate = DateTime.Now;
                                kpiAchievementNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiAchievements.Add(kpiAchievementNew);
                            }
                            else
                            {
                                var kpiAchievementNew = new KpiAchievement();
                                kpiAchievementNew.Id = kpiAchievement.Id;
                                kpiAchievementNew.Value = kpiAchievement.Value;
                                kpiAchievementNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiAchievementNew.PeriodeType = periodeType;
                                kpiAchievementNew.Periode = kpiAchievement.Periode;
                                kpiAchievementNew.IsActive = true;
                                kpiAchievementNew.Remark = kpiAchievement.Remark;
                                kpiAchievementNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiAchievements.Attach(kpiAchievementNew);
                                DataContext.Entry(kpiAchievementNew).State = EntityState.Modified;
                            }
                        }
                    }
                }
                response.IsSuccess = true;
                response.Message = "KPI Achievements has been updated successfully";
                DataContext.SaveChanges();
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }


        public UpdateKpiAchievementItemResponse UpdateKpiAchievementItem(UpdateKpiAchievementItemRequest request)
        {
            var response = new UpdateKpiAchievementItemResponse();
            try
            {
                var kpiAchievement = request.MapTo<KpiAchievement>();

                if (request.Id != 0)
                {
                    DataContext.KpiAchievements.Attach(kpiAchievement);
                    DataContext.Entry(kpiAchievement).State = EntityState.Modified;
                    DataContext.SaveChanges();
                }
                else
                {
                    kpiAchievement.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == request.KpiId);
                    DataContext.KpiAchievements.Add(kpiAchievement);
                    DataContext.SaveChanges();
                }
                response.Id = request.Id;
                response.IsSuccess = true;
                response.Message = "KPI Achievement item has been updated successfully";
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }
    }
}
