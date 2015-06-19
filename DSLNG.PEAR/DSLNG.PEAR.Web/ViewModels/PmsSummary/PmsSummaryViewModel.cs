using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsSummaryViewModel
    {
        public string Osp { get; set; }
        public string PerformanceIndicator { get; set; }
        public double OspWeight { get; set; }
        public decimal Unit { get; set; }
        public decimal Weight { get; set; }

        public decimal TargetYearly { get; set; }
        public decimal TargetMonthly { get; set; }
        public decimal TargetYtd { get; set; }

        public decimal ActualYearly { get; set; }
        public decimal ActualMonthly { get; set; }
        public decimal ActualYtd { get; set; }

        public string IndexYearly
        {
            get { return (ActualYearly / TargetYearly).ToString("0.00"); }
        }

        public string IndexMonthly {
            get { return (ActualMonthly / TargetMonthly).ToString("0.00"); }
        }

        public string IndexYtd
        {
            get { return (ActualYtd/TargetYtd).ToString("0.00"); }
        }

        public double Score { get; set; }

        public string TargetActualYearly
        {
            get { return string.Format(@"{0} / {1}", TargetYearly.ToString("0.00"), ActualYearly.ToString()); }
        }

        public string TargetActualMonthly
        {
            get { return string.Format(@"{0} / {1}", TargetMonthly.ToString("0.00"), ActualMonthly.ToString()); }
        }

        public string TargetActualYtd
        {
            get { return string.Format(@"{0} / {1}", TargetYtd.ToString("0.00"), ActualYtd.ToString()); }
        }
    }
}