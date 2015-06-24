using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using System;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.Type;
using DSLNG.PEAR.Services.Responses.Type;

namespace DSLNG.PEAR.Services
{
    public class TypeService : BaseService, ITypeService
    {
        public TypeService(IDataContext DataContext) : base(DataContext) {}
        public GetTypeResponse GetType(GetTypeRequest request){
            try
            {
                var type = DataContext.Types.First(x => x.Id == request.Id);
                var response = type.MapTo<GetTypeResponse>(); 

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetTypeResponse
                    {
                        IsSuccess = false,
                        Message = x.Message
                    };
            }
        }
        public GetTypesResponse GetTypes(GetTypesRequest request){
            var types = DataContext.Types.ToList();
            var response = new GetTypesResponse();
            response.Types = types.MapTo<GetTypesResponse.Type>();

            return response;
        }

        public CreateTypeResponse Create(CreateTypeRequest request)
        {
            var response = new CreateTypeResponse();
            try
            {
                var type = request.MapTo<Data.Entities.Type>();
                DataContext.Types.Add(type);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI type item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateTypeResponse Update(UpdateTypeRequest request)
        {
            var response = new UpdateTypeResponse();
            try
            {
                var type = request.MapTo<Data.Entities.Type>();
                DataContext.Types.Attach(type);
                DataContext.Entry(type).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI Type item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeleteTypeResponse Delete(int id)
        {
            var response = new DeleteTypeResponse();
            try
            {
                var type = new Data.Entities.Type { Id = id };
                DataContext.Types.Attach(type);
                DataContext.Entry(type).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI type item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
