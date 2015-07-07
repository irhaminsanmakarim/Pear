using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class ChartViewModel
    {
        public int Id { get; set; }
        public int MeasurementId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}