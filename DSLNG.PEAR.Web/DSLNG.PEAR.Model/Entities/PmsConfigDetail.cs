using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Model.Entities
{
    public class PmsConfigDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PmsConfig PmsConfig { get; set; }
        public Kpi Kpi { get; set; }
        public int Weight { get; set; }
        public ScoringType ScoringType { get; set; }
        public bool AsGraphic { get; set; }
        public ScoreIndicator ScoreIndicator { get; set; }
        public IEnumerable<KpiTarget> KpiTargets { get; set; }
        public TargetType TargetType { get; set; }

        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
