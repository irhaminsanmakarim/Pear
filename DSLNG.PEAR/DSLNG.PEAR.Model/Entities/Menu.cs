using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
        public int Order { get; set; }
        public bool IsRoot { get; set; }
        public IEnumerable<RoleGroup> RoleGroups { get; set; }
        public string Remark { get; set; }
        public string Module { get; set; }

        public bool IsActive { get; set; }
    }
}
