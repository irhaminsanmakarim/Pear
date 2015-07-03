using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Common.PmsSummary;

namespace DSLNG.PEAR.Services.Requests.PmsSummary
{
    public class CreatePmsConfigDetailsRequest
    {
        public int PmsConfigId { get; set; }
        public int KpiId { get; set; }
        public string ScoringType { get; set; }
        public int Weight { get; set; }
        public IEnumerable<ScoreIndicator> ScoreIndicators { get; set; }
    }
}
