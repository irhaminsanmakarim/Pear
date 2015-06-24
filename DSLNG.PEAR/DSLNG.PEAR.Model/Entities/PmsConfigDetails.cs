using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Data.Entities
{
    public class PmsConfigDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PmsConfig PmsConfig { get; set; }
        public Kpi Kpi { get; set; }
        public int Weight { get; set; }
        public ScoringType ScoringType { get; set; }
        public bool AsGraphic { get; set; }
        public ICollection<ScoreIndicator> ScoreIndicators { get; set; } 
        //public ScoreIndicator ScoreIndicator { get; set; }
        //public ICollection<KpiTarget> KpiTargets { get; set; }
        //public ICollection<KpiAchievement> KpiAchievements { get; set; }
        public TargetType TargetType { get; set; }

        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
