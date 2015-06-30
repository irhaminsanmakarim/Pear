using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Conversion
{
    public class IndexConversionViewModel
    {
        public int Id { get; set; }
        public Measurement From { get; set; }
        public string FromName { get; set; }
        public Measurement To { get; set; }
        public string ToName { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public bool IsReverse { get; set; }
        public bool IsActive { get; set; }
    }


}