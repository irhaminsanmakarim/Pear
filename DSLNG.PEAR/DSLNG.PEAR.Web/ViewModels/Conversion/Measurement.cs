using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.Conversion
{
    public class Measurement
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}