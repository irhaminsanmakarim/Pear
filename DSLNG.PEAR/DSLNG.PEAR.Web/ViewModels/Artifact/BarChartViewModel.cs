using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class BarChartViewModel
    {
        public BarChartViewModel() {
            PeriodeTypes = new List<SelectListItem>();
            ValueAxises = new List<SelectListItem>();
            SeriesList = new List<Series>();
            Start = DateTime.Now;
            End = DateTime.Now;
            SeriesTypes = new List<SelectListItem>();
            KpiList = new List<SelectListItem>();
            Aggragations = new List<SelectListItem>();
        }
        public string PeriodeType { get; set; }
        public IList<SelectListItem> PeriodeTypes { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [Display(Name="Start")]
        public int StartYear { get; set; }
        [Display(Name = "End")]
        public int EndYear { get; set; }
        public string ValueAxis {get;set;}
        public IList<SelectListItem> ValueAxises { get; set; }
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }
        public IList<Series> SeriesList { get; set; }
        public IList<SelectListItem> SeriesTypes { get; set; }
        public IList<SelectListItem> KpiList { get; set; }
        public IList<SelectListItem> Aggragations { get; set; }
        public class Series
        {
            public Series() {
                Stacks = new List<Stack>();
            }
            public int KpiId { get; set; }
            public string Label { get; set; }
            public string ValueAxis { get; set; }
            public IList<Stack> Stacks { get; set; }
            public string Aggregation { get; set; }
        }

        public class Stack
        {
            public int KpiId { get; set; }
            public string Label { get; set; }
            public string Aggregation { get; set; }
        }
    }
}