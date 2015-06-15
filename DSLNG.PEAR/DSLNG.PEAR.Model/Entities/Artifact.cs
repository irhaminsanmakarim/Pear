using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Artifact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Header { get; set; }
        public ICollection<ArtifactSerie> Series { get; set; }

        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
