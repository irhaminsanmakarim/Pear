using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Measurement From { get; set; }
        public Measurement To { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public bool IsReverse { get; set; }
        public bool IsActive { get; set; }
    }
}
