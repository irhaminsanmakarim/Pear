using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetScoreIndicatorsResponse : BaseResponse
    {
        public IEnumerable<ScoreIndicator> ScoreIndicators { get; set; }

        public class ScoreIndicator
        {
            public string Color { get; set; }
            public string Expression { get; set; }
        }
    }
}
