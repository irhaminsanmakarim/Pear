
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using EPeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class ChartViewModel
    {
        public class SeriesViewModel {
            public SeriesViewModel() {
                Stacks = new List<StackViewModel>();
            }
            [Display(Name = "Kpi")]
            [Required]
            public int KpiId { get; set; }
            [Required]
            public string Label { get; set; }
            public string ValueAxis { get; set; }
            public IList<StackViewModel> Stacks { get; set; }
        }
        public class StackViewModel {
            [Display(Name = "Kpi")]
            [Required]
            public int KpiId { get; set; }
            [Required]
            public string Label { get; set; } 
        }
    }
}