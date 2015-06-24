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

        public GetMenusResponse GetMenus(GetMenusRequest request)
        {
            var menus = DataContext.Menus.ToList();
            var response = new GetMenusResponse();
            response.Menus = menus.MapTo<GetMenusResponse.Menu>();

            return response;
        }

        public GetMenuResponse GetMenu(GetMenuRequest request)
        {
            try
            {
                var menu = DataContext.Menus.First(x => x.Id == request.Id);
                var response = menu.MapTo<GetMenuResponse>(); 

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetMenuResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }
    }
}
