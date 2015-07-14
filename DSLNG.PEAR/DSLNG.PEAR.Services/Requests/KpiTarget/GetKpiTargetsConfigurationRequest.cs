using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class GetKpiTargetsConfigurationRequest
    {
        public string PeriodeType { get; set; }
        public int RoleGroupId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
