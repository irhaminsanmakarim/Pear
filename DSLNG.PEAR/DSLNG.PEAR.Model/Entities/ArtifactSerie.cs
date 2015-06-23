using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class ArtifactSerie
    {
        public ArtifactSerie() {
            ArtifactStacks = new List<ArtifactStack>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Label { get; set; }
        public ICollection<ArtifactStack> ArtifactStacks { get; set; }

        public string Aggregation { get; set; }
        public string Color { get; set; }
        public Kpi Kpi { get; set; }

        public bool IsActive { get; set; }
    }
}
