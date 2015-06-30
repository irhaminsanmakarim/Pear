using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Conversion
{
    public class UpdateConversionViewModel
    {
        public UpdateConversionViewModel()
        {
            MeasurementList = new List<SelectListItem>();
        }

        [Required]
        public int Id { get; set; }
        [Display(Name = "Measurement From")]
        public int MeasurementFrom { get; set; }
        [Display(Name = "Measurement To")]
        public int MeasurementTo { get; set; }
        public float Value { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Is Reverse")]
        public bool IsReverse { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public List<SelectListItem> MeasurementList { get; set; }
    }
}