

using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class TrafficLightChartDataViewModel
    {
        public TrafficLightChartDataViewModel()
        {
            PlotBands = new List<PlotBandViewModel>();
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ValueAxisTitle { get; set; }
        public SeriesViewModel Series { get; set; }
        public IList<PlotBandViewModel> PlotBands { get; set; }
        public class SeriesViewModel
        {
            public SeriesViewModel() {
                data = new List<double>();
            }
            public string name { get; set; }
            public IList<double> data { get; set; }
        }
        public class PlotBandViewModel
        {
            public double from { get; set; }
            public double to { get; set; }
            public string color { get; set; }
            public string label { get; set; }
        }
    }
}