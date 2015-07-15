using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Menu
{
    public class GetSiteMenuActiveRequest
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Url { get; set; }
    }
}
