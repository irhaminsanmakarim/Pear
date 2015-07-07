using System.Collections.Generic;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsConfigDetails
{
    public class UpdatePmsConfigDetailsViewModel
    {
        public int Id { get; set; }
        public int PillarId { get; set; }
        public string PillarName { get; set; }
        public int KpiId { get; set; }
        public string KpiName { get; set; }
        public double Weight { get; set; }
        public string ScoringType { get; set; }
        public IEnumerable<SelectListItem> ScoringTypes { get; set; }
        public IEnumerable<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}