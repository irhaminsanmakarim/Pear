using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetPmsSummaryListResponse : BaseResponse
    {
        public IList<PmsSummary> PmsSummaryList { get; set; }

        public class PmsSummary
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Year { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
