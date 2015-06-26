using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Level
{
    public class CreateLevelRequest
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
