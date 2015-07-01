using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.KpiTarget
{
    public class GetKpiTargetsResponse
    {
        public IList<KpiTarget> KpiTargets { get; set; }

        public class KpiTarget
        {
            public int Id { get; set; }
            public string KpiName { get; set; }
            public string PeriodeType { get; set; }
            public double? Value { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
