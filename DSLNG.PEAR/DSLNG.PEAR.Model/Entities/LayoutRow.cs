using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class LayoutRow
    {
        public LayoutRow() {
            LayoutColumns = new List<LayoutColumn>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Index { get; set; }
        public ICollection<LayoutColumn> LayoutColumns { get; set; }

        public bool IsActive { get; set; }
        //public User CreatedBy { get; set; }
        //public User UpdatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
