using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Measurement
{
    public class CreateMeasurementRequest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Remark { get; set; }
    }
}
