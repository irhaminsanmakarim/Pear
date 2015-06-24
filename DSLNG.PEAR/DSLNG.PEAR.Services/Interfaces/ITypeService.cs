using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface ITypeService
    {
        GetTypeResponse GetType(GetTypeRequest request);
        GetTypesResponse GetTypes(GetTypesRequest request);
        CreateTypeResponse Create(CreateTypeRequest request);
        UpdateTypeResponse Update(UpdateTypeRequest request);
        DeleteTypeResponse Delete(int id);
    }
}
