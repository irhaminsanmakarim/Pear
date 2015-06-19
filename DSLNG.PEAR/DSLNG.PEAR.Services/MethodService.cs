using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Requests.Method;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services
{
    public class MethodService : BaseService, IMethodService
    {
        public MethodService(IDataContext dataContext) : base(dataContext)
        {

        }



        public GetMethodResponse GetMethod(GetMethodRequest request)
        {
            throw new NotImplementedException();
        }

        public GetMethodsResponse GetMethods(GetMethodsRequest requests)
        {
            throw new NotImplementedException();
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
