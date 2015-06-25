using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsConfigDetailsViewModel
    {
        public Kpi GroupKpi { get; set; }
        public List<KpiAchievment> Remarks { get; set; }

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
    }
}