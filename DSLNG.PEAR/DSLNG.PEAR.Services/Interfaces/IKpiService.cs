using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Kpi;


namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IKpiService
    {
        GetKpiResponse GetBy(GetKpiRequest request);
        GetKpiToSeriesResponse GetKpiToSeries();
    }
}
