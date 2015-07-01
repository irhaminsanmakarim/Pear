using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Artifact
    {
        public Artifact() {
            Series = new List<ArtifactSerie>();
            Plots = new List<ArtifactPlot>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string GraphicType { get; set; }
        [Required]
        public string GraphicName { get; set; }
        [Required]
        public string HeaderTitle { get; set; }

        public ICollection<ArtifactSerie> Series { get; set; }
        public ICollection<ArtifactPlot> Plots { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public ValueAxis ValueAxis { get; set; }
        public RangeFilter RangeFilter { get; set; }
        public Measurement Measurement { get; set; }
        public double FractionScale { get; set; }
        public double MaxValue { get; set; }

        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate.HasValue
                   ? this.createdDate.Value
                   : DateTime.Now;
            }

            set { this.createdDate = value; }
        }
        private DateTime? createdDate = null;
        private DateTime? updatedDate = null;
        public DateTime UpdatedDate
        {
            get
            {
                return this.updatedDate.HasValue
                   ? this.updatedDate.Value
                   : DateTime.Now;
            }

            set { this.updatedDate = value; }
        }
    }
}
