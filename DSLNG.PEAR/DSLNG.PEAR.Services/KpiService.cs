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
using DSLNG.PEAR.Data.Entities;
using System.Data.Entity.Infrastructure;

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


        public GetKpiToSeriesResponse GetKpiToSeries()
        {
            return new GetKpiToSeriesResponse
            {
                KpiList = DataContext.Kpis.ToList()
                .MapTo<GetKpiToSeriesResponse.Kpi>()
            };
        }

        public GetKpiResponse GetKpi(GetKpiRequest request)
        {
            try
            {
                var kpi = DataContext.Kpis.First(x => x.Id == request.Id);
                var response = kpi.MapTo<GetKpiResponse>(); 

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetKpiResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }
        public GetKpisResponse GetKpis(GetKpisRequest request)
        {
            var kpis = DataContext.Kpis.ToList();
            var response = new GetKpisResponse();
            response.Kpis = kpis.MapTo<GetKpisResponse.Kpi>();

            return response;
        }

        public CreateKpiResponse Create(CreateKpiRequest request)
        {
            var response = new CreateKpiResponse();
            try
            {
                var kpi = request.MapTo<Kpi>();
                DataContext.Kpis.Add(kpi);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
