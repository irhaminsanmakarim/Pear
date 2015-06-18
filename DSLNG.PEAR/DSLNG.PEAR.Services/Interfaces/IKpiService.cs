using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IKpiService
    {
        GetKpiResponse GetBy(GetKpiRequest request);

    }
}
