using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class ScoreIndicator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public int RefId { get; set; }
        public string Color { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

        public bool IsActive { get; set; }
    }
}
