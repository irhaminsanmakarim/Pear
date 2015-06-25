using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetPmsSummaryResponse : BaseResponse
    {
        public GetPmsSummaryResponse()
        {
            KpiDatas = new List<KpiData>();
        }

        public IList<KpiData> KpiDatas { get; set; }

        public class KpiData
        {
            public int Id { get; set; }
            public string Osp { get; set; }
            public string PerformanceIndicator { get; set; }
            public double OspWeight { get; set; }
            public string Unit { get; set; }
            public decimal Weight { get; set; }

            public decimal TargetYearly { get; set; }
            public decimal TargetMonthly { get; set; }
            public decimal TargetYtd { get; set; }

            public decimal? ActualYearly { get; set; }
            public decimal? ActualMonthly { get; set; }
            public decimal? ActualYtd { get; set; }

            /*public string TargetYearly { get; set; }
            public string TargetMonthly { get; set; }
            public string TargetYtd { get; set; }

            public string ActualYearly { get; set; }
            public string ActualMonthly { get; set; }
            public string ActualYtd { get; set; }

            public string IndexYearly { get; set; }
            public string IndexMonthly { get; set; }
            public string IndexYtd { get; set; }*/
        }
    }
}
