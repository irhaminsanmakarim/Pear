using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMeasurementService
    {
        GetMeasurementResponse GetMeasurement(GetMeasurementRequest request);
        GetMeasurementsResponse GetMeasurements(GetMeasurementsRequest request);
        void Add(GetMeasurementInsert request);
        void Save(GetMeasurementUpdate request);
        void Delete(GetMeasurementRequest request);
    }
}
