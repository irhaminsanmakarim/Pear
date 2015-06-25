using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class PmsSummaryService : BaseService, IPmsSummaryService
    {
        public PmsSummaryService(IDataContext dataContext)
            : base(dataContext)
        {

        }

        public Responses.PmsSummary.GetPmsSummaryResponse GetPmsSummary(Requests.PmsSummary.GetPmsSummaryRequest request)
        {
            var pmsSummaries = new List<PmsSummary>();
            var yearNow = DateTime.Now.Year;
            pmsSummaries = DataContext.PmsSummaries.Where(x => x.Title == yearNow.ToString()).ToList();
            var response = new GetPmsSummaryResponse();
            response.PmsSummaries = pmsSummaries.MapTo<GetPmsSummaryResponse.PmsSummary>();
            return response;
        }
    }
}
