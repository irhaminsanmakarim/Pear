using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetPieDataResponse
    {
        public GetPieDataResponse()
        {
            SeriesResponses = new List<SeriesResponse>();
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IList<SeriesResponse> SeriesResponses { get; set; }
        public class SeriesResponse
        {
            public string name { get; set; }
            public double? y { get; set; }
            public string color { get; set; }
            public string measurement { get; set; }
        }
    }
}
