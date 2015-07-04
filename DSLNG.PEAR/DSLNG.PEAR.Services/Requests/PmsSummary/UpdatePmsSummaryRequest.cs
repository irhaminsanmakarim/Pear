

using System.Collections.Generic;
using DSLNG.PEAR.Services.Common.PmsSummary;

namespace DSLNG.PEAR.Services.Requests.PmsSummary
{
    public class UpdatePmsSummaryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public IList<ScoreIndicator> ScoreIndicators { get; set; }
        public bool IsActive { get; set; }
    }
}
