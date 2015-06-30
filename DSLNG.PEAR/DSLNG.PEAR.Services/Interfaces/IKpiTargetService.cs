using DSLNG.PEAR.Services.Requests.KpiTarget;
using DSLNG.PEAR.Services.Responses.KpiTarget;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IKpiTargetService
    {
        CreateKpiTargetResponse Create(CreateKpiTargetRequest request);
    }
}
