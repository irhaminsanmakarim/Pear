using System.Collections.Generic;
using System.Web.Mvc;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsSummaryDetailsViewModel
    {
        public IList<SelectListItem> Pillars { get; set; }
        //public PillarSelectList PillarSelect { get;set;}
        public IList<SelectListItem> Kpis { get; set; }
        public IList<PmsConfig> PmsConfigs { get; set; }
        public int[] PillarIds { get; set; }
        public int PmsSummaryId { get; set; }
        /*public class Pillar
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }*/

        /*public class Kpi
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Measurement { get; set; }
        }*/

        public class PmsConfig
        {
            public PmsConfig()
            {
                PmsConfigDetailsList = new List<PmsConfigDetails>();
            }

            public int Id { get; set; }
            public int PillarId { get; set; }
            public string PillarName { get; set; }
            public double Weight { get; set; }
            public ScoringType ScoringType { get; set; }
            public IList<PmsConfigDetails> PmsConfigDetailsList { get; set; }
        }

        public class PmsConfigDetails
        {
            //public Kpi Kpi { get; set; }
            public int Id { get; set; }
            public int KpiId { get; set; }
            public string KpiName { get; set; }
            public double Weight { get; set; }
            public string KpiMeasurement { get; set; }
            public string ScoringType { get; set; }
            //public bool KpiAsGraphic { get; set; }
            public IList<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
        }

       
    }
}