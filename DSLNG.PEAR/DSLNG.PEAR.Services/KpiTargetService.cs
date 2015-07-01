using DSLNG.PEAR.Data.Entities;
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
                                            .FirstOrDefault(x=>x.Id == request.Id);
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
                kpis = DataContext.KpiTargets.Include(x=>x.Kpi).OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take).ToList();
            }
            else
            {
                kpis = DataContext.KpiTargets.Include(x=>x.Kpi).ToList();
            }
            var response = new GetKpiTargetsResponse();
            response.KpiTargets = kpis.MapTo<GetKpiTargetsResponse.KpiTarget>();

            return response;
        }
    }
}
