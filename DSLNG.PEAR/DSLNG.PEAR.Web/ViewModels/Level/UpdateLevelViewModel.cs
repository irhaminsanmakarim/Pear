using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Level
{
    public class UpdateLevelViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Order")]
        public int Number { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}