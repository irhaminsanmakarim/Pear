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
                    kpiData.Osp = pmsConfig.Pillar.Name;
                    kpiData.PerformanceIndicator = pmsConfigDetails.Kpi.Name;
                    kpiData.Unit = pmsConfigDetails.Kpi.Measurement.Name;
                    kpiData.Weight = pmsConfigDetails.Weight;

                    #region KPI Achievement

                    var kpiAchievementYearly = pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly);
                    if (kpiAchievementYearly != null && kpiAchievementYearly.Value != null)
                        kpiData.ActualYearly = kpiAchievementYearly.Value.Value;
                    

                    var kpiAchievementMonthly = 
                        pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month);
                    if (kpiAchievementMonthly == null)
                    {
                        kpiData.ActualMonthly = null;
                    }
                    else
                    {
                        kpiData.ActualMonthly = kpiAchievementMonthly.Value.HasValue ? kpiAchievementMonthly.Value : null;
                    }

                    var kpiAchievementYtd =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(
                            x => x.Periode.Month >= 1 && x.Periode.Month <= request.Month);
                    foreach (var achievementYtd in kpiAchievementYtd)
                    {
                        if(kpiData.ActualYtd.HasValue)
                            kpiData.ActualYtd += achievementYtd.Value;
                    }

                    #endregion

                    #region KPI Target

                    /*var kpiTargetYearly = pmsConfigDetails.Kpi.KpiTargets;
                    foreach (var targetYearly in kpiTargetYearly)
                    {
                        if (targetYearly.Value.HasValue)
                            kpiData.TargetYearly += targetYearly.Value;
                    }

                    var kpiTargetMonthly =
                        pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.Periode.Month == request.Month);
                    if (kpiTargetMonthly == null)
                    {
                        kpiData.ActualMonthly = null;
                    }
                    else
                    {
                        kpiData.ActualMonthly = kpiAchievementMonthly.Value.HasValue ? kpiAchievementMonthly.Value : null;
                    }

                    var kpiAchievementYtd =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(
                            x => x.Periode.Month >= 1 && x.Periode.Month <= request.Month);
                    foreach (var achievementYtd in kpiAchievementYtd)
                    {
                        if (kpiData.ActualYtd.HasValue)
                            kpiData.ActualYtd += achievementYtd.Value;
                    }*/

                    #endregion
                        /*kpiData.ActualMonthly =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(x => x.Periode.Month == request.Month)
                                        .Sum(x => x.Value != null ? x.Value.Value : 0);
                    kpiData.ActualYearly = pmsConfigDetails.Kpi.KpiAchievements.Sum(x => x.Value.Value);
                    kpiData.ActualYtd =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(
                            x => x.Periode.Month > 0 && x.Periode.Month <= request.Month).Sum(x => x.Value.Value);
                    kpiData.TargetMonthly =
                        pmsConfigDetails.Kpi.KpiTargets.Where(x => x.Periode.Month == request.Month)
                                        .Sum(x => x.Value.Value);

                    kpiData.TargetYearly = pmsConfigDetails.Kpi.KpiTargets.Sum(x => x.Value.Value);
                    kpiData.TargetYtd =
                        pmsConfigDetails.Kpi.KpiTargets.Where(
                            x => x.Periode.Month > 0 && x.Periode.Month <= request.Month).Sum(x => x.Value.Value);*/
                    /*kpiData.IndexMonthly = (Decimal.Parse(kpiData.ActualMonthly)/Decimal.Parse(kpiData.TargetMonthly)).ToString();
                    kpiData.IndexYearly = (Decimal.Parse(kpiData.ActualYearly) / Decimal.Parse(kpiData.TargetYearly)).ToString();
                    kpiData.IndexYtd = (Decimal.Parse(kpiData.ActualYtd) / Decimal.Parse(kpiData.TargetYtd)).ToString();*/
                    response.KpiDatas.Add(kpiData);
                }
            }

            return response;

            //return new GetPmsSummaryResponse();
        }
    }
}
