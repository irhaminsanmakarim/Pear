using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Common.PmsSummary;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetScoreIndicatorsResponse : BaseResponse
    {
        public IEnumerable<ScoreIndicator> ScoreIndicators { get; set; }
    }
}
