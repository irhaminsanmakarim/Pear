using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class MeasurementService : BaseService, IMeasurementService
    {
        public MeasurementService(IDataContext dataContext): base(dataContext)
        {

        }

        public GetMeasurementResponse GetMeasurement(GetMeasurementRequest request)
        {
            var response = new GetMeasurementResponse();
            try
            {
                var measurement = DataContext.Measurements.First(x => x.Id == request.Id);
                response = measurement.MapTo<GetMeasurementResponse>();
                response.IsSuccess = true;
                response.Message = "Measurement item has been updated successfully";
            }
            catch (ArgumentNullException nullException)
            {
                response.Message = nullException.Message;
            }

            return response;

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

        public CreateMeasurementResponse Create(CreateMeasurementRequest request)
        {
            var response = new CreateMeasurementResponse();
            try
            {
                var measurement = request.MapTo<Measurement>();
                DataContext.Measurements.Add(measurement);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Measurement item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateMeasurementResponse Update(UpdateMeasurementRequest request)
        {
            var response = new UpdateMeasurementResponse();
            try
            {
                var measurement = request.MapTo<Measurement>();
                DataContext.Measurements.Attach(measurement);
                DataContext.Entry(measurement).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Measurement item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
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
