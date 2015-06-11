using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Model.Entities
{
    public class PmsConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Pilar Pilar { get; set; }
        public int Wight { get; set; }
        public ScoringType ScoringType { get; set; }
        public ScoreIndicator ScoreIndicator { get; set; }
        public bool IsActive { get; set; }
    }
}
