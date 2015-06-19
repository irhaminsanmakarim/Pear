using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSLNG.PEAR.Services.Responses
{
    public class ConversionResponse
    {
        public int Id { get; set; }
        public MeasurementResponse From { get; set; }
        public MeasurementResponse To { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public bool IsReverse { get; set; }
        public bool IsActive { get; set; }
    }
}
