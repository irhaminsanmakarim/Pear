using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.ArtifactDesigner
{
    public class BarChartViewModel
    {
        public string PeriodeType { get; set; }
        public IList<SelectListItem> PeriodeTypes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ValueAxis {get;set;}
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }
        public IList<Series> Series { get; set; }

        public class Series
        {
            public int KpiId { get; set; }
            public string Label { get; set; }
            public string ValueAxis { get; set; }
            public IList<Stack> Stacks { get; set; }
        }

        public class Stack
        {
            public int KpiId { get; set; }
            public string Label { get; set; }
        }
    }
}