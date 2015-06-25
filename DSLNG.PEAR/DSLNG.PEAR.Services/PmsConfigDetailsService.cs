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
                .FirstOrDefault(x => x.Id == request.Id);
            response.GroupKpi = config.Kpi.MapTo<GetPmsConfigDetailsResponse.KpiData>();
            response.GroupKpi.ActualYearly = config.Kpi.KpiAchievements.Sum(x => x.Value.Value);
            response.GroupKpi.ActualMonthly = config.Kpi.KpiAchievements.Where(x => x.Periode.Month == request.Month).Sum(x => x.Value.Value);
            var kpiAchievments = config.Kpi.KpiAchievements.OrderBy(x => x.Periode);
            response.KpiAchievments = new List<GetPmsConfigDetailsResponse.KpiAchievmentMonthly>();

            foreach (var item in kpiAchievments)
            {
                response.KpiAchievments.Add(new GetPmsConfigDetailsResponse.KpiAchievmentMonthly
                {
                    Period = item.Periode.ToString("MMM", CultureInfo.InvariantCulture),
                    Remark = item.Remark
                });
            }
            return response;
        }
    }
}
