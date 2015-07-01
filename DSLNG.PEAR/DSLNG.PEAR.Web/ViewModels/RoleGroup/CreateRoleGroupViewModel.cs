using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.RoleGroup
{
    public class CreateRoleGroupViewModel
    {
        public CreateRoleGroupViewModel()
        {
            LevelList = new List<SelectListItem>();
        }
        [Required]
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Remark { get;set; }
        public bool IsActive { get; set; }
        [Display(Name = "Select Level")]
        public int LevelId { get; set; }
        public List<SelectListItem> LevelList { get; set; }
    }
}