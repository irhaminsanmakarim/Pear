using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Data.Entities
{
    public class Kpi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int? PilarId { get; set; } //to make this nullable we need to include it
        public Pilar Pilar { get; set; }
        public Level Level { get; set; }
        public RoleGroup RoleGroup { get; set; }
        public Type Type { get; set; }
        public Group Group { get; set; }
        public bool IsEconomic { get; set; }
        public int Order { get; set; }
        public YtdFormula YtdFormula { get; set; }
        public Measurement Measurement { get; set; }
        public Method Method { get; set; }
        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }
        public Periode Periode { get; set; }
        public string Remark { get; set; }
        public ICollection<KpiRelationModel> RelationModels { get; set; }
        public DateTime? Value { get; set; }

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
