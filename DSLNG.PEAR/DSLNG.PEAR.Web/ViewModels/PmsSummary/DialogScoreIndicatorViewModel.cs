

using System.Collections.Generic;
using DSLNG.PEAR.Web.ViewModels.Common;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class DialogScoreIndicatorViewModel
    {
        public IEnumerable<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}