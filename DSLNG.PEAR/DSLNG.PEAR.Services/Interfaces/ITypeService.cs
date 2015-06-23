using DSLNG.PEAR.Services.Requests.Type;
using DSLNG.PEAR.Services.Responses.Type;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface ITypeService
    {
        GetTypeResponse GetType(GetTypeRequest request);
        GetTypesResponse GetTypes(GetTypesRequest request);
    }
}
