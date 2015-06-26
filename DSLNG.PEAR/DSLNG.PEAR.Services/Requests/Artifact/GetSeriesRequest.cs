

using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Requests.Artifact
{
    public class GetSeriesRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public RangeFilter RangeFilter {get;set;}
        public ValueAxis ValueAxis { get; set; }
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }
        public IList<Series> SeriesList { get; set; }

        public class Series
        {
            public Series()
            {
                Stacks = new List<Stack>();
            }
            public int KpiId { get; set; }
            public string Label { get; set; }
            public ValueAxis ValueAxis { get; set; }
            public IList<Stack> Stacks { get; set; }
        }

        public class Stack
        {
            public int KpiId { get; set; }
            public string Label { get; set; }
        }
    }
}
