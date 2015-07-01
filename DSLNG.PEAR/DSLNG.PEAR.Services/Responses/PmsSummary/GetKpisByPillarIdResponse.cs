using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetKpisByPillarIdResponse : BaseResponse
    {
        public IEnumerable<Kpi> Kpis { get; set; }
        public class Kpi
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}
