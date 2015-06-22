using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Type
{
    class GetTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}
