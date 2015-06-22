using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using System;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Measurement;
using System.Collections.Generic;

namespace DSLNG.PEAR.Services
{
    public class MeasurementService : BaseService, IMeasurementService
    {
        public MeasurementService(IDataContext dataContext): base(dataContext)
        {

        }

        public GetMeasurementsResponse GetMeasurements(GetMeasurementsRequest request)
        {
            var measurements = new List<Measurement>();
            if (request.Take != 0)
            {
                measurements = DataContext.Measurements.OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take).ToList();                
            }
            else
            {
                measurements = DataContext.Measurements.OrderBy(x => x.Id).ToList();
            }
            var response = new GetMeasurementsResponse();
            response.Measurements = measurements.MapTo<GetMeasurementsResponse.Measurement>();
            return response;
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

        public DeleteMeasurementResponse Delete(int id)
        {
            var response = new DeleteMeasurementResponse();
            try
            {
                var measurement = new Measurement {Id = id};
                DataContext.Measurements.Attach(measurement);
                DataContext.Entry(measurement).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Measurement item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
