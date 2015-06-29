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
    }
}
