
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class ComboChartViewModel
    {
        public ComboChartViewModel()
        {
            Charts = new List<ChartViewModel>();
            ValueAxes = new List<SelectListItem>();
            GraphicTypes = new List<SelectListItem>();
        }
        public IList<SelectListItem> ValueAxes { get; set; }
        public IList<SelectListItem> GraphicTypes { get; set; }
        public IList<ChartViewModel> Charts { get; set; }
        public class ChartViewModel
        {
            [Display(Name = "Value Axis")]
            public string ValueAxis { get; set; }
            [Display(Name = "Graphic Type")]
            public string GraphicType { get; set; }
            public BarChartViewModel BarChart { get; set; }
            public LineChartViewModel LineChart { get; set; }
            public AreaChartViewModel AreaChart { get; set; }
        }
    }
}