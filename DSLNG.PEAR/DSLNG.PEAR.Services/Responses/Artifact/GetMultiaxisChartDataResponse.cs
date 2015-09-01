﻿

using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetMultiaxisChartDataResponse : BaseResponse
    {
        public GetMultiaxisChartDataResponse() {
            Charts = new List<ChartResponse>();
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Periodes { get; set; }
        public IList<ChartResponse> Charts { get; set; }
        public class ChartResponse
        {
            public ChartResponse() {
                Series = new List<SeriesViewModel>();
            }
            public string ValueAxisTitle { get; set; }
            public string ValueAxisColor { get; set; }
            public bool IsOpposite { get; set; }
            public string Measurement { get; set; }
            public string GraphicType { get; set; }
            public string SeriesType { get; set; }
            public IList<SeriesViewModel> Series { get; set; }

            public class SeriesViewModel
            {
                public string name { get; set; }
                public IList<double?> data { get; set; }
                public string stack { get; set; }
                public string color { get; set; }
            }
        }
    }
}
