using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsConfigDetailsViewModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public Kpi GroupKpi { get; set; }
        public List<KpiAchievment> RemarksMonthly { get; set; }
        public KpiAchievment RemarksYearly { get; set; }
        public List<KpiRelationModel> ClausalModel { get; set; }

        public class Kpi
        {
            public string Group { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
            public string Period { get; set; }
            public decimal ActualYearly { get; set; }
            public decimal ActualMonthly { get; set; }
        }

        public class KpiAchievment
        {
            public string Type { get; set; }
            public string Period { get; set; }
            public string Remark { get; set; }
        }

        public class KpiRelationModel
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public string RelationModel { get; set; }
            public decimal ActualYearly { get; set; }
            public decimal ActualMonthly { get; set; }
        }
    }
}