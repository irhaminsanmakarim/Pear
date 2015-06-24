using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Type
{
    public class CreateTypeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}