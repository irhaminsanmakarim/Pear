using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class PieDataViewModel
    {
        public PieDataViewModel()
        {
            SeriesResponses = new List<SeriesResponse>();
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool Is3D { get; set; }
        public bool ShowLegend { get; set; }
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