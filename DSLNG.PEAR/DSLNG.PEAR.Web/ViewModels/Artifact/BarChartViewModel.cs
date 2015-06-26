using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Data.Enums;
using EPeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;
using System.Globalization;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class BarChartViewModel
    {
        public BarChartViewModel() {
            PeriodeTypes = new List<SelectListItem>();
            ValueAxes = new List<SelectListItem>();
            SeriesList = new List<Series>();
            SeriesTypes = new List<SelectListItem>();
            KpiList = new List<SelectListItem>();
            RangeFilters = new List<SelectListItem>();
        }
        [Display(Name="Periode Type")]
        public string PeriodeType { get; set; }
        public IList<SelectListItem> PeriodeTypes { get; set; }
        [Display(Name="Range Filter")]
        public string RangeFilter { get; set; }
        public IList<SelectListItem> RangeFilters { get; set; }
        public DateTime Start { get {
            if (this.PeriodeType == EPeriodeType.Monthly.ToString()) {
                return DateTime.ParseExact("01/" + this.StartInDisplay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (this.PeriodeType == EPeriodeType.Yearly.ToString()) {
                return DateTime.ParseExact("01/01/" + this.StartInDisplay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (this.PeriodeType == EPeriodeType.Daily.ToString() || this.PeriodeType == EPeriodeType.Weekly.ToString())
            {
                return DateTime.ParseExact(this.StartInDisplay, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return DateTime.ParseExact(this.StartInDisplay, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }}
        public DateTime End { get {
            if (this.PeriodeType == EPeriodeType.Monthly.ToString()) {
                return DateTime.ParseExact("01/" + this.EndInDisplay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (this.PeriodeType == EPeriodeType.Yearly.ToString()) {
                return DateTime.ParseExact("01/01/" + this.EndInDisplay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (this.PeriodeType == EPeriodeType.Daily.ToString() || this.PeriodeType == EPeriodeType.Weekly.ToString())
            {
                return DateTime.ParseExact(this.EndInDisplay, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return DateTime.ParseExact(this.EndInDisplay, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }}
        [Display(Name = "Start")]
        public string StartInDisplay { get; set; }
        [Display(Name = "End")]
        public string EndInDisplay { get; set; }
        [Display(Name = "Value Axis")]
        public string ValueAxis {get;set;}
        public IList<SelectListItem> ValueAxes { get; set; }
        [Display(Name = "Fraction Scale")]
        public double FractionScale { get; set; }
        [Display(Name = "Maximum Scale")]
        public double MaxValue { get; set; }
        public IList<Series> SeriesList { get; set; }
        public IList<SelectListItem> SeriesTypes { get; set; }
        public IList<SelectListItem> KpiList { get; set; }
        public class Series
        {
            public Series() {
                Stacks = new List<Stack>();
            }
            [Display(Name="Kpi")]
            public int KpiId { get; set; }
            public string Label { get; set; }
            public string ValueAxis { get; set; }
            public IList<Stack> Stacks { get; set; }
        }

        public class Stack
        {
            [Display(Name="Kpi")]
            public int KpiId { get; set; }
            public string Label { get; set; }
        }
    }
}