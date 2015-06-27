using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsConfigDetails;
using DSLNG.PEAR.Services.Responses.PmsConfigDetails;
using System.Data.Entity;
using DSLNG.PEAR.Common.Extensions;
using System.Globalization;

namespace DSLNG.PEAR.Services
{
    public class PmsConfigDetailsService : BaseService, IPmsConfigDetailsService
    {
        public PmsConfigDetailsService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetPmsConfigDetailsResponse GetPmsConfigDetails(GetPmsConfigDetailsRequest request)
        {
            var response = new GetPmsConfigDetailsResponse();
            var config = DataContext.PmsConfigDetails.Include(x => x.PmsConfig.PmsSummary)
                .Include(x => x.Kpi)
                .Include(x => x.Kpi.Group)
                .Include(x => x.Kpi.KpiAchievements)
                .Include(x => x.Kpi.Measurement)
                .Include(x => x.Kpi.Periode)
                .Include(x => x.Kpi.RelationModels)
                .Include(x => x.Kpi.RelationModels.Select(y => y.Kpi))
                .Include(x => x.Kpi.RelationModels.Select(y => y.Kpi).Select(z => z.KpiAchievements))
                .Include(x => x.Kpi.RelationModels.Select(y => y.Kpi).Select(z => z.Measurement))
                .FirstOrDefault(x => x.Id == request.Id);

            if (config != null)
            {
                response.Title = config.PmsConfig.PmsSummary.Title;
                response.Year = config.PmsConfig.PmsSummary.Year;
                response.KpiGroup = config.Kpi.Group != null ? config.Kpi.Group.Name : "";
                response.KpiName = config.Kpi.Name;
                response.KpiUnit = config.Kpi.Measurement != null ? config.Kpi.Measurement.Name : "";
                response.KpiPeriod = config.Kpi.Periode != null ? config.Kpi.Periode.Name.ToString() : "";
                var kpiActualYearly = config.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == Data.Enums.PeriodeType.Yearly);
                if (kpiActualYearly != null)
                {
                    response.KpiActualYearly = kpiActualYearly.Value;
                    response.KpiPeriodYearly = kpiActualYearly.Periode.Year.ToString();
                    response.KpiTypeYearly = kpiActualYearly.PeriodeType.ToString();
                    response.KpiRemarkYearly = kpiActualYearly.Remark;
                }
                var kpiActualMonthly = config.Kpi.KpiAchievements.Where(x => x.PeriodeType == Data.Enums.PeriodeType.Monthly);
                response.KpiAchievmentMonthly = new List<GetPmsConfigDetailsResponse.KpiAchievment>();
                if (kpiActualMonthly != null)
                {
                    var kpiActualMonth = kpiActualMonthly.FirstOrDefault(x => x.Periode.Month == request.Month);
                    response.KpiActualMonthly = kpiActualMonth != null ? kpiActualMonth.Value : null;
                    response.KpiAchievmentMonthly = kpiActualMonthly.MapTo<GetPmsConfigDetailsResponse.KpiAchievment>();
                }

                response.KpiRelations = new List<GetPmsConfigDetailsResponse.KpiRelation>();
                var kpiRelationModel = config.Kpi.RelationModels;
                if (kpiRelationModel != null)
                {
                    foreach (var item in kpiRelationModel)
                    {
                        var actualYearly = item.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == Data.Enums.PeriodeType.Yearly);
                        var actualMonthly = item.Kpi.KpiAchievements.Where(x => x.PeriodeType == Data.Enums.PeriodeType.Monthly);
                        response.KpiRelations.Add(new GetPmsConfigDetailsResponse.KpiRelation
                        {
                            Name = item.Kpi.Name,
                            Unit = item.Kpi.Measurement.Name,
                            Method = item.Method,
                            ActualYearly = actualYearly != null ? actualYearly.Value : null,
                            ActualMonthly = actualMonthly != null ? actualMonthly.Sum(x => x.Value) : null
                        });
                    }
                }

            }

            return response;
        }
    }
}
