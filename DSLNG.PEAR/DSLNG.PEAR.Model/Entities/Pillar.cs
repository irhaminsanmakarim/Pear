using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Pillar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
