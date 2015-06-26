using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using System.Data.Entity;

namespace DSLNG.PEAR.Services
{
    public class PmsSummaryService : BaseService, IPmsSummaryService
    {
        public PmsSummaryService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetPmsSummaryResponse GetPmsSummary(GetPmsSummaryRequest request)
        {
            var response = new GetPmsSummaryResponse();
            var pmsSummary = DataContext.PmsSummaries
                .Include("PmsConfigs.Pillar")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Measurement")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiTargets")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiAchievements")
                .First(x => x.IsActive == true && x.Year == request.Year);

            
            foreach (var pmsConfig in pmsSummary.PmsConfigs)
            {
                foreach (var pmsConfigDetails in pmsConfig.PmsConfigDetailsList)
                {
                    var kpiData = new GetPmsSummaryResponse.KpiData();
                    kpiData.Id = pmsConfigDetails.Id;
                    kpiData.Pillar = pmsConfig.Pillar.Name;
                    kpiData.PerformanceIndicator = pmsConfigDetails.Kpi.Name;
                    kpiData.Unit = pmsConfigDetails.Kpi.Measurement.Name;
                    kpiData.Weight = pmsConfigDetails.Weight;

                    #region KPI Achievement

                    var kpiAchievementYearly = pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly);
                    if (kpiAchievementYearly != null && kpiAchievementYearly.Value != null)
                        kpiData.ActualYearly = kpiAchievementYearly.Value.Value;
                    

                    var kpiAchievementMonthly = 
                        pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month);
                    if (kpiAchievementMonthly != null && kpiAchievementMonthly.Value.HasValue)
                        kpiData.ActualMonthly = kpiAchievementMonthly.Value.Value;
                    
                    
                    var kpiAchievementYtd = pmsConfigDetails.Kpi.KpiAchievements.Where(
                        x => x.PeriodeType == PeriodeType.Monthly && (x.Periode.Month >= 1 && x.Periode.Month <= request.Month)).ToList();
                    if (kpiAchievementYtd.Count > 0) kpiData.ActualYtd = 0;
                    foreach (var achievementYtd in kpiAchievementYtd)
                    {
                        if (achievementYtd.Value.HasValue)
                            kpiData.ActualYtd += achievementYtd.Value;
                    }

                    #endregion
                    
                    #region KPI Target

                    var kpiTargetYearly = pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly);
                    if (kpiTargetYearly != null && kpiTargetYearly.Value != null)
                        kpiData.TargetYearly = kpiTargetYearly.Value.Value;


                    var kpiTargetMonthly =
                        pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month);
                    if (kpiTargetMonthly != null && kpiTargetMonthly.Value.HasValue)
                        kpiData.TargetMonthly = kpiTargetMonthly.Value.Value;


                    var kpiTargetYtd = pmsConfigDetails.Kpi.KpiTargets.Where(
                        x => x.PeriodeType == PeriodeType.Monthly && (x.Periode.Month >= 1 && x.Periode.Month <= request.Month)).ToList();
                    if (kpiTargetYtd.Count > 0) kpiData.TargetYtd = 0;
                    foreach (var targetYtd in kpiTargetYtd)
                    {
                        if (targetYtd.Value.HasValue)
                            kpiData.TargetYtd += targetYtd.Value;
                    }

                    #endregion

                    #region Score
                    if (kpiData.ActualYtd.HasValue && kpiData.TargetYtd.HasValue)
                    {
                        var indexYtd = (kpiData.ActualYtd.Value / kpiData.TargetYtd.Value);
                    
                        switch (pmsConfigDetails.ScoringType)
                        {
                            case ScoringType.Positive:
                                kpiData.Score = pmsConfigDetails.Weight * indexYtd;
                                break;
                            case ScoringType.Negative:
                                kpiData.Score = pmsConfigDetails.Weight / indexYtd;
                                break;
                            case ScoringType.Custom:
                                bool isMoreThanZero = false;
                                var kpiAchievement = pmsConfigDetails.Kpi.KpiAchievements.Where(x => x.Value.HasValue).ToList();
                                bool isNull = kpiAchievement.Count > 0;
                                foreach (var achievement in kpiAchievement)
                                {
                                    if (achievement.Value > 0)
                                        isMoreThanZero = true;
                                }


                                if (!isNull)
                                {
                                    kpiData.Score = isMoreThanZero ? 0 : Double.Parse(kpiData.Weight.ToString());
                                }

                                break;
                        }
                        
                    }
                    

                    #endregion

                    response.KpiDatas.Add(kpiData);
                }
            }

            return response;

            //return new GetPmsSummaryResponse();
        }
    }
}
