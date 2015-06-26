using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSLNG.PEAR.Services.Responses.Kpi
{
    public class Periode
    {
        public int Id { get; set; }

        public PeriodeType Name { get; set; }
        public DateTime? Value { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }

}
