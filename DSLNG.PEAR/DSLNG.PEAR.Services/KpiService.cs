using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
