using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Conversion
{
    public class CreateConversionRequest
    {
        public int MeasurementFrom { get; set; }
        public int MeasurementTo { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public bool IsReverse { get; set; }
        public bool IsActive { get; set; }
    }
}
