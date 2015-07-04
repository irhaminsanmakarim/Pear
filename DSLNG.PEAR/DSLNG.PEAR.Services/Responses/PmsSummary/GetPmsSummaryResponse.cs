using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Common.PmsSummary;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetPmsSummaryResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public IEnumerable<ScoreIndicator> ScoreIndicators { get; set; }
        public bool IsActive { get; set; }
    }
}
