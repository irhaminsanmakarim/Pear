using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DSLNG.PEAR.Web.ViewModels.Periode
{
    public class CreatePeriodeViewModel
    {
        [Required]
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