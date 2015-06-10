using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Model.Entities
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
        public Entities.Type Type { get; set; }
        public Group Group { get; set; }
        public string IsEconomic { get; set; }
        public int Order { get; set; }
        public YtdFormula YtdFormula { get; set; }
        public Measurement Measurement { get; set; }
        public Method Method { get; set; }
        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }
        public Periode Periode { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}
