using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Common;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.CorporatePortofolio
{
    public class CreatePmsConfigDetailsViewModel
    {
        public CreatePmsConfigDetailsViewModel()
        {
            ScoreIndicators = new List<ScoreIndicatorViewModel>()
                {
                    new ScoreIndicatorViewModel()
                };
        }

        public string PillarName { get; set; }
        public IEnumerable<SelectListItem> Kpis { get; set; }
        public int KpiId { get; set; }
        public double Weight { get; set; }
        public string ScoringType { get; set; }
        public IEnumerable<SelectListItem> ScoringTypes { get; set; }
        public IList<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
    }
}