using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Menu
{
    public class UpdateMenuViewModel
    {
        public UpdateMenuViewModel()
        {
            RoleGroupOptions = new List<SelectListItem>();
            MenuOptions = new List<SelectListItem>();
        }

        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public int Order { get; set; }
        public string Remark { get; set; }
        public string Module { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "Select Role Groups")]
        public List<int> RoleGroupIds { get; set; }
        [Display(Name = "Parent Menu")]
        public int? ParentId { get; set; }
        public List<SelectListItem> RoleGroupOptions { get; set; }
        public List<SelectListItem> MenuOptions { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
    }
}