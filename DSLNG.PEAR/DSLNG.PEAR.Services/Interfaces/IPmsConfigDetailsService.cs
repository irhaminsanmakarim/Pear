using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Requests.PmsConfigDetails;
using DSLNG.PEAR.Services.Responses.PmsConfigDetails;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IPmsConfigDetailsService
    {
        GetPmsConfigDetailsResponse GetPmsConfigDetails(GetPmsConfigDetailsRequest request);
    }
}
