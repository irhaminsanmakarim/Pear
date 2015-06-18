using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSLNG.PEAR.Web.ViewModels.Menu
{
    public class RoleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }

    public class Level
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }

}
