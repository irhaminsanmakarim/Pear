using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class KpiAchievement : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Kpi Kpi { get; set; }
        //public PmsConfigDetails PmsConfigDetail { get; set; }
        public int Value { get; set; }
        public DateTime Periode { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
        
    }
}
