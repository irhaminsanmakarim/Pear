using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class DashboardTemplate
    {
        public DashboardTemplate() {
            LayoutRows = new List<LayoutRow>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LayoutContent { get; set; }
        public int RefershTime { get; set; } //in minutes
        public string Remark { get; set; }
        public ICollection<LayoutRow> LayoutRows { get; set; }

        public bool IsActive { get; set; }
        //public User CreatedBy { get; set; }
        //public User UpdatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
