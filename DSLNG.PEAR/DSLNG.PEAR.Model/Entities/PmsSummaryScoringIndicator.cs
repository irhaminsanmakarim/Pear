using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Data.Entities
{
    public class PmsSummaryScoringIndicator
    {
        public PmsSummaryScoringIndicator()
        {
            ScoreIndicators = new Collection<ScoreIndicator>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PmsSummaryScoringIndicatorType Type { get; set; }
        public ICollection<ScoreIndicator> ScoreIndicators { get; set; }
    }
}
