using AutoMapper;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Services.Responses.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(IDataContext dataContext) : base(dataContext)
        {

        }

        public GetMenuResponse GetMenus(GetMenuRequest request)
        {
            var level = DataContext.Levels.First(x => x.Id == request.Id);
            var menu = DataContext.Menus.First(x => x.Id == request.Id);
            var response = Mapper.Map<GetMenuResponse>(menu);
            return response;
        }
    }
}
