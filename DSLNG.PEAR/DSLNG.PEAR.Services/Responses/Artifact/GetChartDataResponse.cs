

using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetChartDataResponse
    {
        public GetChartDataResponse()
        {
            Series = new List<SeriesResponse>();
        }
        public string SeriesType { get; set; }
        public string[] Periodes { get; set; }
        public IList<SeriesResponse> Series { get; set; }
        public class SeriesResponse
        {
            public SeriesResponse() {
                data = new List<double>();
            }
            public string name { get; set; }
            public IList<double> data { get; set; }
            public string stack { get; set; }
        }
    }
}
