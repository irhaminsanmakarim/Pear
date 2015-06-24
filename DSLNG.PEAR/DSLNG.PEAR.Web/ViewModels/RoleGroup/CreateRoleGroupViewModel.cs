using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.RoleGroup
{
    public class CreateRoleGroupViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Remark { get;set; }
        public bool IsActive { get; set; }
    }
}