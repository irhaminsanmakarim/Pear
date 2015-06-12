using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services
{
    public class LevelService : BaseService, ILevelService
    {
        public LevelService(IDataContext dataContext) : base(dataContext)
        {

        }
        public GetLevelResponse GetLevel(GetLevelRequest request)
        {
            var level = DataContext.Levels.First(x => x.Id == request.Id);
            var response = new GetLevelResponse
            {
                Code = level.Code,
                Name = level.Name,
                Number = level.Number
            };

            return response;
        }
    }
}
