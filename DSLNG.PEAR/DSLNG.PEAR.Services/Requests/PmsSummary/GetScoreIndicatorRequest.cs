using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.PmsSummary
{
    public class GetScoreIndicatorRequest
    {
        public int PmsConfigDetailId { get; set; }
        public int PmsSummaryId { get; set; }
    }
}
