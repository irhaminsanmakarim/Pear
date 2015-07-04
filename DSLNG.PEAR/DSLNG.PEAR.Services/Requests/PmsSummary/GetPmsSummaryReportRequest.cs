using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.PmsSummary
{
    public class GetPmsSummaryReportRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
