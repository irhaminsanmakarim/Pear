using System;
using System.Collections.Generic;
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

        public decimal IndexYearly { get; set; }
        public decimal IndexMonthly { get; set; }
        public decimal IndexYtd { get; set; }

        public double Score { get; set; }
    }
}