

using DSLNG.PEAR.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSLNG.PEAR.Data.Entities
{
    public class ArtifactChart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GraphicType { get; set; }
        public ICollection<ArtifactSerie> Series { get; set; }
        public ICollection<ArtifactPlot> Plots { get; set; }
        public ValueAxis ValueAxis { get; set; }
        public Measurement Measurement { get; set; }
    }
}
