using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    kpiData.ActualMonthly =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(x => x.Periode.Month == request.Month)
                                        .Sum(x => x.Value);
                    kpiData.ActualYearly = pmsConfigDetails.Kpi.KpiAchievements.Sum(x => x.Value);
                    kpiData.ActualYtd =
                        pmsConfigDetails.Kpi.KpiAchievements.Where(
                            x => x.Periode.Month > 0 && x.Periode.Month <= request.Month).Sum(x => x.Value);
                    kpiData.TargetMonthly =
                        pmsConfigDetails.Kpi.KpiTargets.Where(x => x.Periode.Month == request.Month)
                                        .Sum(x => x.Value);

                    kpiData.TargetYearly = pmsConfigDetails.Kpi.KpiTargets.Sum(x => x.Value);
                    kpiData.TargetYtd =
                        pmsConfigDetails.Kpi.KpiTargets.Where(
                            x => x.Periode.Month > 0 && x.Periode.Month <= request.Month).Sum(x => x.Value);
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
