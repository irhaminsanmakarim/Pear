using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Services.Responses.PmsSummary;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IPmsSummaryService
    {
        GetPmsSummaryResponse GetPmsSummary(GetPmsSummaryRequest request);
    }
}
