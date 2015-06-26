using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Kpi
{
    public class Conversion
    {
        public int Id { get; set; }
        public Measurement From { get; set; }
        public Measurement To { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public bool IsReverse { get; set; }
        public bool IsActive { get; set; }
    }
}
