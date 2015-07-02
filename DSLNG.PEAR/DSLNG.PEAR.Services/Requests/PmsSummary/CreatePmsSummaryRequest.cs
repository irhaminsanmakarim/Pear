using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Common.PmsSummary;

namespace DSLNG.PEAR.Services.Requests.PmsSummary
{
    public class CreatePmsSummaryRequest
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public IList<ScoreIndicator> ScoreIndicators { get; set; }
        public bool IsActive { get; set; }
    }
}
