using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Group
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
