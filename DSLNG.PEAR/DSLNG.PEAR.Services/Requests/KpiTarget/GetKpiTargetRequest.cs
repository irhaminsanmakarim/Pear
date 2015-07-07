using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class GetKpiTargetRequest
    {
        public int PmsSummaryId { get; set; }
        public PeriodeType PeriodeType { get; set; }
    }
}
