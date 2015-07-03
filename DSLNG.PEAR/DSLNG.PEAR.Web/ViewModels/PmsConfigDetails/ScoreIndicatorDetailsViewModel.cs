using System.Collections.Generic;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsConfigDetails
{
    public class ScoreIndicatorDetailsViewModel
    {
        public IEnumerable<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}