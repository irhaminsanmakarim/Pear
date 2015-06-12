using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Responses.Pillar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services
{
    public class PillarService : BaseService, IPillarService
    {
        public PillarService(IDataContext dataContext):base(dataContext)
        {

        }
        public GetPillarResponse GetPillar(GetPillarRequest request)
        {
            var pillar = DataContext.Pilars.FirstOrDefault(x => x.Id == request.Id);
            var response = new GetPillarResponse
            {
                Code = pillar.Code,
                Name = pillar.Name,
                Color = pillar.Color,

            };
            return response;
        }
    }
}
