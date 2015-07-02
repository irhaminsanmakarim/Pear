

using System.Collections.Generic;
using DSLNG.PEAR.Web.ViewModels.PmsSummary.Common;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class DialogScoreIndicatorViewModel
    {
        public IEnumerable<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}