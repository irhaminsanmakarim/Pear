using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.KpiAchievement
{
    public class ConfigurationParamViewModel
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string PeriodeType { get; set; }
    }
}