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
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using System.Data.Entity;

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
                var menu = DataContext.Menus.Include(x => x.RoleGroups).First(x => x.Id == request.Id);
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

        public CreateMenuResponse Create(CreateMenuRequest request)
        {
            var response = new CreateMenuResponse();
            try
            {
                var menu = request.MapTo<Data.Entities.Menu>();
                //set IsRoot if no menu as parent
                menu.IsRoot = request.ParentId <= 0;

                //check if has role group
                if (request.RoleGroupIds.Count > 0)
                {
                    foreach (int roleGroupId in request.RoleGroupIds)
                    {
                        var roleGroup = DataContext.RoleGroups.FirstOrDefault(r => r.Id == roleGroupId);
                        
                        //add selected role group to menu
                        menu.RoleGroups.Add(roleGroup);
                    }
                }
                
                DataContext.Menus.Add(menu);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Menu item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateMenuResponse Update(UpdateMenuRequest request)
        {
            var response = new UpdateMenuResponse();
            try
            {
                var menu = request.MapTo<Data.Entities.Menu>();
                //set IsRoot if no menu as parent
                menu.IsRoot = request.ParentId <= 0;

                //check if has role group
                if (request.RoleGroupIds.Count > 0)
                {
                    foreach (int roleGroupId in request.RoleGroupIds)
                    {
                        var roleGroup = DataContext.RoleGroups.FirstOrDefault(r => r.Id == roleGroupId);

                        //add selected role group to menu
                        menu.RoleGroups.Add(roleGroup);
                    }
                }
                else
                {
                    menu.RoleGroups = null;
                }

                DataContext.Menus.Attach(menu);
                DataContext.Entry(menu).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Menu item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeleteMenuResponse Delete(int id)
        {
            var response = new DeleteMenuResponse();
            response.Id = id;
            try
            {
                var menu = new Data.Entities.Menu { Id = id };
                DataContext.Menus.Attach(menu);
                DataContext.Entry(menu).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Menu item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
