using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsSummaryIndexViewModel
    {
        public IEnumerable<PmsSummaryViewModel> PmsSummaries { get; set; }
    }
}