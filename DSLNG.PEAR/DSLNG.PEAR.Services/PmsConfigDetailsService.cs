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
            var config = DataContext.PmsConfigDetails.Include("PmsConfig.PmsSummary")
                .Include("Kpi")
                .Include("Kpi.Group")
                .Include("Kpi.KpiTargets")
                .Include("Kpi.KpiAchievements")
                .Include("Kpi.Measurement")
                .Include("Kpi.Periode")
                .Include("Kpi.RelationModels")
                .Include("Kpi.RelationModels.Kpi")
                .Include("Kpi.RelationModels.Kpi.KpiAchievements")
                .Include("Kpi.RelationModels.Kpi.Measurement")
                .FirstOrDefault(x => x.Id == request.Id);
            response.GroupKpi = config.Kpi.MapTo<GetPmsConfigDetailsResponse.KpiData>();
            response.GroupKpi.ActualYearly = config.Kpi.KpiAchievements.Sum(x => x.Value.Value);
            response.GroupKpi.ActualMonthly = config.Kpi.KpiAchievements.Where(x => x.Periode.Month == request.Month).Sum(x => x.Value.Value);
            var kpiAchievmentsMonthly = config.Kpi.KpiAchievements.Where(x => x.PeriodeType == Data.Enums.PeriodeType.Monthly).OrderBy(x => x.Periode);
            var kpiAchievmentsYearly = config.Kpi.KpiAchievements.First(x => x.PeriodeType == Data.Enums.PeriodeType.Yearly);
            response.KpiAchievments = new List<GetPmsConfigDetailsResponse.KpiAchievment>();

            foreach (var item in kpiAchievmentsMonthly)
            {
                response.KpiAchievments.Add(new GetPmsConfigDetailsResponse.KpiAchievment
                {
                    Period = item.Periode.ToString("MMM", CultureInfo.InvariantCulture),
                    Remark = item.Remark,
                    Type = item.PeriodeType.ToString()
                });
            }

            response.KpiAchievmentYearly.Period = kpiAchievmentsYearly.Periode.ToString("yyyy");
            response.KpiAchievmentYearly.Type = kpiAchievmentsYearly.PeriodeType.ToString();
            response.KpiAchievmentYearly.Remark = kpiAchievmentsYearly.Remark;
            response.KpiRelations = new List<GetPmsConfigDetailsResponse.KpiRelation>();
            var kpiRelationModel = config.Kpi.RelationModels;
            foreach (var item in kpiRelationModel)
            {
                response.KpiRelations.Add(new GetPmsConfigDetailsResponse.KpiRelation
                    {
                        Name = item.Kpi.Name,
                        Unit = item.Kpi.Measurement.Name,
                        RelationModel = item.Method,
                        ActualYearly = item.Kpi.KpiAchievements.Sum(x => x.Value.Value),
                        ActualMonthly = item.Kpi.KpiAchievements.Where(x => x.Periode.Month == request.Month).Sum(x => x.Value.Value)
                    });
            }
            return response;
        }
    }
}
