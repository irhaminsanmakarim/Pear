
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EPeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;
using System.Globalization;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class SpeedometerChartViewModel
    {
        public SpeedometerChartViewModel()
        {
            PeriodeTypes = new List<SelectListItem>();
            ValueAxes = new List<SelectListItem>();
            KpiList = new List<SelectListItem>();
            RangeFilters = new List<SelectListItem>();
            Series = new SeriesViewModel();
            PlotBands = new List<PlotBand>();
            PlotBands.Add(new PlotBand());
        }
        [Display(Name="Periode Type")]
        public string PeriodeType { get; set; }
        public IList<SelectListItem> PeriodeTypes { get; set; }
        [Display(Name="Range Filter")]
        public string RangeFilter { get; set; }
        public IList<SelectListItem> RangeFilters { get; set; }
        public DateTime? Start { get {
            if (string.IsNullOrEmpty(this.StartInDisplay)) {
                return null;
            }
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
        public DateTime? End { get {
            if (string.IsNullOrEmpty(this.EndInDisplay))
            {
                return null;
            }
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
        public SeriesViewModel Series { get; set; }
        public IList<SelectListItem> KpiList { get; set; }
        public IList<PlotBand> PlotBands { get; set; }
        public class SeriesViewModel
        {
            [Display(Name = "Kpi")]
            public int KpiId { get; set; }
            public string Label { get; set; }
        }
        public class PlotBand {
            public double From { get; set; }
            public double To { get; set; }
            public string Color { get; set; }
        }

    }
}