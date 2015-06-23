using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Type
{
    public class GetTypesResponse : BaseResponse
    {
        public IList<GetTypeResponse> Types { get; set; }
    }
}
