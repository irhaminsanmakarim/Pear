using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class KpiService : BaseService, IKpiService
    {
        public KpiService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetKpiResponse GetBy(GetKpiRequest request)
        {
            var query = DataContext.Kpis;
            if (request.Id != 0) {
                query.Where(x => x.Id == request.Id);
            }
            return query.FirstOrDefault().MapTo<GetKpiResponse>();
        }


        public GetKpiToSeriesResponse GetKpiToSeries(GetKpiToSeriesRequest request)
        {
            return new GetKpiToSeriesResponse
            {
                KpiList = DataContext.Kpis.Where(x => x.Name.Contains(request.Filter))
                .OrderBy(x => x.Name)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList()
                .MapTo<GetKpiToSeriesResponse.Kpi>()
            };
        }
    }
}
