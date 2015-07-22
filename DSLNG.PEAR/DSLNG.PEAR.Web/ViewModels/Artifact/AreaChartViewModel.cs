

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class AreaChartViewModel
    {
        public AreaChartViewModel()
        {
            Series = new List<SeriesViewModel>();
        }
        public IList<SeriesViewModel> Series { get; set; }
        public class SeriesViewModel
        {
            [Display(Name = "Kpi")]
            public int KpiId { get; set; }
            public string KpiName { get; set; }
            public string Label { get; set; }
            public string Color { get; set; }
        }
    }
}