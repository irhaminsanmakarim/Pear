using DSLNG.PEAR.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class ArtifactStack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Label { get; set; }
        public Kpi Kpi {get;set;}
        //public string Aggregation {get;set;}
        public ValueAxis ValueAxis { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
    }
}
