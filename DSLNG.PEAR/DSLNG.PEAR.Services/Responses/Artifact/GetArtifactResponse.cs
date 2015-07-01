

using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetArtifactResponse
    {
        public int Id { get; set; }
        public string GraphicType { get; set; }
        public string GraphicName { get; set; }
        public string HeaderTitle { get; set; }

        public IList<SeriesResponse> Series { get; set; }
        public IList<SeriesResponse> PlotBands { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public ValueAxis ValueAxis { get; set; }
        public RangeFilter RangeFilter { get; set; }
        public string Measurement { get; set; }
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }

        public class SeriesResponse {
            public SeriesResponse()
            {
                Stacks = new List<StackResponse>();
            }
            public int KpiId { get; set; }
            public string Label { get; set; }
            public ValueAxis ValueAxis { get; set; }
            public IList<StackResponse> Stacks { get; set; }
        }
        public class StackResponse
        {
            public int KpiId { get; set; }
            public string Label { get; set; }
        }
        public class PlotResponse {
            public double From { get; set; }
            public double To { get; set; }
            public string Color { get; set; }
        }

    }
}
