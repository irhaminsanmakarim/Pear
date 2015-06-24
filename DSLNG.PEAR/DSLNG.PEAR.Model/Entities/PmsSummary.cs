using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class PmsSummary : BaseEntity
    {
        public PmsSummary()
        {
            ScoreIndicators = new Collection<ScoreIndicator>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public Periode Periode { get; set; }
        public ICollection<ScoreIndicator> ScoreIndicators { get; set; }
        public ICollection<PmsConfig> PmsConfigs { get; set; }
        //TODO : what is PMS tar on board?

        public bool IsActive { get; set; }
        
    }
}
