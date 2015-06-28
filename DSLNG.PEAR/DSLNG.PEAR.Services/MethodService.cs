using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Requests.Method;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class MethodService : BaseService, IMethodService
    {
        public MethodService(IDataContext dataContext) : base(dataContext)
        {

        }



        public GetMethodResponse GetMethod(GetMethodRequest request)
        {
            try
            {
                var method = DataContext.Methods.First(x => x.Id == request.Id);
                var response = method.MapTo<GetMethodResponse>();

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetMethodResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }

        public GetMethodsResponse GetMethods(GetMethodsRequest requests)
        {
            var methods = DataContext.Methods.ToList();
            var response = new GetMethodsResponse();
            response.Methods = methods.MapTo<GetMethodsResponse.Method>();
            return response;
        }

        public void Add(AddMethod request)
        {
            throw new NotImplementedException();
        }

        public void Save(SaveMethod request)
        {
            throw new NotImplementedException();
        }
    }
}
