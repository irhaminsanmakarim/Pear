﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Measurement
{
    public class GetMeasurementResponse
    {
        //public MeasurementResponse Unit { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }

    public class GetMeasurementsResponse {
        public IList<GetMeasurementResponse> Units { get; set; }
    }

    
}
