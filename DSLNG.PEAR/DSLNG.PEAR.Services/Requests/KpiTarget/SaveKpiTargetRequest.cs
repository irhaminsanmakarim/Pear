using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class SaveKpiTargetRequest
    {
        public int Id { get; set; }
        public int KpiId { get; set; }
        public DateTime Periode { get; set; }
        public double? Value { get; set; }
        public string Remark { get; set; }
        public DSLNG.PEAR.Data.Enums.PeriodeType PeriodeType { get; set; }
    }
}
