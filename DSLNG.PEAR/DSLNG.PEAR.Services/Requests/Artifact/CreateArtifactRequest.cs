

using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Requests.Artifact
{
    public class CreateArtifactRequest
    {
        public CreateArtifactRequest() {
            Series = new List<SeriesRequest>();
            Plots = new List<PlotRequest>();
        }
        public int Id { get; set; }
        public string GraphicName { get; set; }
        public string GraphicType { get; set; }
        public string HeaderTitle { get; set; }
        public IList<SeriesRequest> Series { get; set; }
        public IList<PlotRequest> Plots { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public ValueAxis ValueAxis { get; set; }
        public RangeFilter RangeFilter { get; set; }
        public int MeasurementId { get; set; }
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }

        public class SeriesRequest
        {
            public SeriesRequest() {
                Stacks = new List<StackRequest>();
            }
            public string Label { get; set; }
            public IList<StackRequest> Stacks { get; set; }
            public string Color { get; set; }
            public int KpiId { get; set; }
            public ValueAxis ValueAxis { get; set; }
        }
        public class PlotRequest
        {
            public double From { get; set; }
            public double To { get; set; }
            public string Color { get; set; }
        }
        public class StackRequest
        {
            public string Label { get; set; }
            public int KpiId { get; set; }
            public ValueAxis ValueAxis { get; set; }
            public string Color { get; set; }

        }
    }
}
