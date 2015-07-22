using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class CreateKpiTargetsRequest
    {
        public List<KpiTarget> KpiTargets { get; set; }

        public class KpiTarget
        {
            public int Id { get; set; }
            public int KpiId { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
            public PeriodeType PeriodeType { get; set; }
            public string Remark { get; set; }

            public bool IsActive { get; set; }
        }
    }
}
