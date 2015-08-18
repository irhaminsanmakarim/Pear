

using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetTrafficLightChartDataResponse
    {
        public GetTrafficLightChartDataResponse()
        {
            PlotBands = new List<PlotBandResponse>();
        }

        public string Subtitle { get; set; }
        public SeriesResponse Series { get; set; }
        public IList<PlotBandResponse> PlotBands { get; set; }
        public class SeriesResponse
        {
            public string name { get; set; }
            public double data { get; set; }
        }
        public class PlotBandResponse
        {
            public double from { get; set; }
            public double to { get; set; }
            public string color { get; set; }
            public string label { get; set; }
        }
    }
}
