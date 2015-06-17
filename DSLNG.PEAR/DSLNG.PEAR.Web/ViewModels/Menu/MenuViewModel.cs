using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSLNG.PEAR.Data.Entities;

namespace DSLNG.PEAR.Web.ViewModels.Menu
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<DSLNG.PEAR.Data.Entities.Menu> Menus { get; set; }
        public int Order { get; set; }
        public bool IsRoot { get; set; }
        public ICollection<RoleGroup> RoleGroups { get; set; }
        public string Remark { get; set; }
        public string Module { get; set; }

        public bool IsActive { get; set; }
    }
}