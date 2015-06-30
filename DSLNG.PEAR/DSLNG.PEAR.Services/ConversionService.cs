using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Conversion;
using DSLNG.PEAR.Services.Responses.Conversion;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace DSLNG.PEAR.Services
{
    public class ConversionService : BaseService, IConversionService
    {
        public ConversionService(IDataContext dataContext) : base (dataContext)
        {

        }

        public GetConversionResponse GetConversion(GetConversionRequest request){
            try
            {
                var conversion = DataContext.Conversions.Include(f => f.From).Include(t => t.To).First(x => x.Id == request.Id);
                //var conversion = DataContext.Conversions.First(x => x.Id == request.Id);
                var response = new GetConversionResponse();
                response = conversion.MapTo<GetConversionResponse>();

                return response;
            }catch (System.InvalidOperationException x){
                return new GetConversionResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }
        public GetConversionsResponse GetConversions(GetConversionsRequest request){
            var conversions = DataContext.Conversions.Include(f => f.From).Include(t => t.To).ToList();
            var response = new GetConversionsResponse();
            response.Conversions = conversions.MapTo<GetConversionsResponse.Conversion>();

            return response;
        }
        public CreateConversionResponse Create(CreateConversionRequest request)
        {
            var response = new CreateConversionResponse();
            try
            {
                var conversion = request.MapTo<Conversion>();
                conversion.From = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementFrom);
                conversion.To = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementTo);

                DataContext.Conversions.Add(conversion);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Conversion item has been added successfully";
            }
            catch (DbUpdateException exception)
            {
                response.IsSuccess = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public UpdateConversionResponse Update(UpdateConversionRequest request){
            var response = new UpdateConversionResponse();
            try
            {
                var conversion = request.MapTo<Conversion>();
                conversion.From = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementFrom);
                conversion.To = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementTo);
                DataContext.Conversions.Attach(conversion);
                DataContext.Entry(conversion).State = EntityState.Modified;
                DataContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Conversion item has been updated successfully";
            }
            catch (DbUpdateException exception)
            {
                response.IsSuccess = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public DeleteConversionResponse Delete(int Id)
        {
            var response = new DeleteConversionResponse();

            try
            {
                var conversion = new Conversion { Id = Id};
                DataContext.Conversions.Attach(conversion);
                DataContext.Entry(conversion).State = EntityState.Deleted;
                DataContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Conversion item has been deleted successfully";
            }
            catch (DbUpdateException exception)
            {
                response.IsSuccess = false;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}
