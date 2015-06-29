using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Group
{
    public class UpdateGroupViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Order { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}