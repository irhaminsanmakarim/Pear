using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class Periode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PeriodeType Name { get; set; }
        public DateTime? Value { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
