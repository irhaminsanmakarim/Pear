using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Menu
    {
        public Menu() {
            Menus = new List<Menu>();
            RoleGroups = new List<RoleGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public int Order { get; set; }
        public bool IsRoot { get; set; }
        public ICollection<RoleGroup> RoleGroups { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public string Module { get; set; }
        public Menu Parent { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
    }
}
