using DSLNG.PEAR.Services.Requests.Method;
using DSLNG.PEAR.Services.Responses.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMethodService
    {
        GetMethodResponse GetMethod(GetMethodRequest request);
        GetMethodsResponse GetMethods(GetMethodsRequest requests);
        void Add(AddMethod request);
        void Save(SaveMethod request);
    }
}
