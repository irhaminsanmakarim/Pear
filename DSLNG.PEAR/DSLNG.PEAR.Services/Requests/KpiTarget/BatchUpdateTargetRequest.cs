using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class BatchUpdateTargetRequest
    {
        public BatchUpdateTargetRequest()
        {
            BatchUpdateKpiTargetItemRequest = new List<SaveKpiTargetRequest>();
        }
        public List<SaveKpiTargetRequest> BatchUpdateKpiTargetItemRequest { get; set; }
    }
}
