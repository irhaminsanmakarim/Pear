using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;

namespace DSLNG.PEAR.Services.Interfaces
{
    public class ITypeService
    {
        GetTypeResponse GetType (GetTypeRequest request);
        GetTypesResponse GetTypes(GetTypesRequest request);
    }
}
