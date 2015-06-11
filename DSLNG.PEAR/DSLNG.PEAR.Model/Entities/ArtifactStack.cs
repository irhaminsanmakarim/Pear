using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Model.Entities
{
    public class ArtifactStack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RefType { get; set; }
        public string RefId { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
    }
}
