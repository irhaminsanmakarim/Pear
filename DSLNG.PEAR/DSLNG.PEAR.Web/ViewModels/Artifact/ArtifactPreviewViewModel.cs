using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class ArtifactPreviewViewModel
    {
        public string GraphicType { get; set; }
        public AreaChartDataViewModel AreaChart { get; set; }
        public BarChartDataViewModel BarChart { get; set; }
        public LineChartDataViewModel LineChart { get; set; }
        public SpeedometerChartDataViewModel SpeedometerChart { get; set; }
    }
}