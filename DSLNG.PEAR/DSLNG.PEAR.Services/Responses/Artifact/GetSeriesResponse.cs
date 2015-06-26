

using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetSeriesResponse
    {
        public GetSeriesResponse() {
            Series = new List<SeriesResponse>();
        }
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
