using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSLNG.PEAR.Services.Responses
{
    public class MeasurementResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
