using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Level
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
