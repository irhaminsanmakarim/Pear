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
    public class LineChartViewModel
    {
        public LineChartViewModel() {
            Series = new List<SeriesViewModel>();
        }
        public IList<SeriesViewModel> Series { get; set; }
        public class SeriesViewModel
        {
            [Display(Name="Kpi")]
            public int KpiId { get; set; }
            public string Label { get; set; }
            public string Color { get; set; }
        }
    }
}