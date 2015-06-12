using AutoMapper;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

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
            var response = Mapper.Map<GetLevelResponse>(level);

            return response;
        }


        public GetLevelsResponse GetLevels(GetLevelsRequest request)
        {
            var levels = DataContext.Levels.ToList();
            var response = new GetLevelsResponse();
            response.Levels = levels.MapTo<GetLevelResponse>();

            return response;
        }
    }
}
