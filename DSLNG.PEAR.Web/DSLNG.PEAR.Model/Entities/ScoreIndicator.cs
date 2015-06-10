using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DSLNG.PEAR.Model.Entities
{
    public class ScoreIndicator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RefId { get; set; }
        public string Color { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

        public bool IsActive { get; set; }
    }
}
