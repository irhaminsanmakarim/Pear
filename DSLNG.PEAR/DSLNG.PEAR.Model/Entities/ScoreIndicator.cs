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
        public string Expression { get; set; }
        /*public double? MinValue { get; set; }
        public double? MaxValue { get; set; }*/

        //public bool IsActive { get; set; }
    }
}

//minvalue 0, dan minvalue not included -> red