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
            public string Pillar { get; set; }
            public string PerformanceIndicator { get; set; }
            public string Unit { get; set; }
            public decimal Weight { get; set; }

            public double? TargetYearly { get; set; }
            public double? TargetMonthly { get; set; }
            public double? TargetYtd { get; set; }

            public double? ActualYearly { get; set; }
            public double? ActualMonthly { get; set; }
            public double? ActualYtd { get; set; }

            public double? IndexYearly
            {
                get
                {
                    if (ActualYearly.HasValue && TargetYearly.HasValue)
                    {
                        if (Math.Abs(ActualYearly.Value) == 0 && TargetYearly.Value == 0)
                            return 1;

                        return ActualYearly/TargetYearly;
                    }

                    return null;
                }
            }

            public double? IndexMonthly
            {
                get
                {
                    if (ActualMonthly.HasValue && TargetMonthly.HasValue)
                    {
                        if (ActualMonthly.Value == 0 && TargetMonthly.Value == 0)
                            return 1;

                        return ActualMonthly / TargetMonthly;
                    }

                    return null;
                }
            }

            public double? IndexYtd
            {
                get
                {
                    if (ActualYtd.HasValue && TargetYtd.HasValue)
                    {
                        if (ActualYtd.Value == 0 && TargetYtd.Value == 0)
                            return 1;

                        return ActualYtd / TargetYtd;
                    }

                    return null;
                }
            }

            public double? Score { get; set; }

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
