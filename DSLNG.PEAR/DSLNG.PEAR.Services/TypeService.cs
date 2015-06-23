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
            var response = new GetTypeResponse();
            try {
                var roleGroup = DataContext.Types.First(x => x.Id == request.Id);
            }catch (ArgumentNullException nullException){
                response.Message = nullException.Message;
            }

            return response;
        }
        public GetTypesResponse GetTypes(GetTypesRequest request){
            var types = DataContext.Types.ToList();
            var response = new GetTypesResponse();
            response.Types = types.MapTo<GetTypesResponse.Type>();

            return response;
        }
    }
}
