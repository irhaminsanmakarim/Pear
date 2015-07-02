using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.PmsSummary.Common;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class DialogPmsConfigDetailViewModel
    {
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Pillars { get; set; }
        public int PillarId { get; set; }
        public IEnumerable<SelectListItem> Kpis { get; set; }
        public int KpiId { get; set; }
        public double Weight { get; set; }
        public string ScoringType { get; set; }
        public IEnumerable<SelectListItem> ScoringTypes { get; set; }
        public IList<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}