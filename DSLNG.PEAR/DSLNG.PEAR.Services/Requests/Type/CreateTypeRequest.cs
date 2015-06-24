using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Type
{
    public class CreateTypeRequest
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
