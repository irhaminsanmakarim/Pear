using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
//using DSLNG.PEAR.Services.Responses.MeasurementResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Measurement;

namespace DSLNG.PEAR.Services
{
    public class MeasurementService : BaseService, IMeasurementService
    {
        public MeasurementService(IDataContext dataContext): base(dataContext)
        {

        }

        public GetMeasurementResponse GetMeasurement(GetMeasurementRequest request)
        {
            throw new NotImplementedException();
        }

        public void Add(GetMeasurementInsert request)
        {
            throw new NotImplementedException();
        }

        public void Save(GetMeasurementUpdate request)
        {
            throw new NotImplementedException();
        }

        public void Delete(GetMeasurementRequest request)
        {
            throw new NotImplementedException();
        }


        public GetMeasurementsResponse GetMeasurements(GetMeasurementsRequest request)
        {
            var units = DataContext.Measurements.ToList();
            var response = new GetMeasurementsResponse();
            response.Units = units.MapTo<GetMeasurementResponse>();
            return response;
        }
    }
}
