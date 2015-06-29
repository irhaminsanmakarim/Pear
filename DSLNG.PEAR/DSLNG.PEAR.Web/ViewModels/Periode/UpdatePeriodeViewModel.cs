using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Periode
{
    public class UpdatePeriodeViewModel
    {
        public int Id { get; set; }
        public PeriodeType Name { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }

        public enum PeriodeType
        {
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly
        }

        public IList<PeriodeType> PeriodeTypeOptions = new List<PeriodeType>{
            PeriodeType.Hourly, PeriodeType.Daily, PeriodeType.Weekly, PeriodeType.Monthly, PeriodeType.Yearly
        };
    }
}