

namespace DSLNG.PEAR.Services.Requests.Kpi
{
    public class GetKpiToSeriesRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Filter { get; set; }
    }
}
