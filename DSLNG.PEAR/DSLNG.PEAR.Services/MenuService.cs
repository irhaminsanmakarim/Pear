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

        public GetSiteMenusResponse GetSiteMenus(GetSiteMenusRequest request)
        {
            var response = new GetSiteMenusResponse();
            var menus = new List<Data.Entities.Menu>();

            if (request.ParentId != null)
            {
                menus = DataContext.Menus.Where(x => x.ParentId == request.ParentId).OrderBy(x => x.Order).ToList();
            }
            else
            {
                menus = DataContext.Menus.Where(x => x.ParentId == null || x.ParentId == 0).OrderBy(x => x.Order).ToList();
            }

            if (request.IncludeChildren)
            {
                //looping to get the children, we dont use Include because only include 1st level child menus
                foreach (var menu in menus)
                {
                    menu.Menus = this._GetMenuChildren(menu.Id);
                }
            }

            response.Menus = menus.MapTo<GetSiteMenusResponse.Menu>();
            //set root menu active / selected
            if (request.MenuId == null || request.MenuId == 0)
            {
                response.MenuIdActive = DataContext.Menus.Where(x => x.ParentId == null || x.ParentId == 0).Select(x => x.Id).First();
            }
            else
            {

            }
            
            return response;
        }

        private ICollection<Data.Entities.Menu> _GetMenuChildren(int ParentId)
        {
            var Menus = new List<Data.Entities.Menu>();

            Menus = DataContext.Menus.Where(x => x.ParentId == ParentId).OrderBy(x => x.Order).ToList();

            if (Menus != null){
                foreach (var menu in Menus)
                {
                    menu.Menus = this._GetMenuChildren(menu.Id);
                }
            }
            

            return Menus;
        }

        public GetSiteMenuActiveResponse GetSiteMenuActive(GetSiteMenuActiveRequest request)
        {
            var response = new GetSiteMenuActiveResponse();
            //get the menu from url request
            var url_request = new StringBuilder(request.Controller).Append("/").Append(request.Action).ToString();
            try{
                var menu = DataContext.Menus.Where(x => x.Url.ToLower() == url_request).First();
                menu = this._GetActiveMenu(menu);
                response = menu.MapTo<GetSiteMenuActiveResponse>();

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                var menu = DataContext.Menus.First(m => m.Id == 1);
                response = menu.MapTo<GetSiteMenuActiveResponse>();
                response.Message = x.Message;
                return response;
            }
        }

        private Data.Entities.Menu _GetActiveMenu(Data.Entities.Menu menu)
        {
            if (!menu.IsRoot)
            {
                menu = DataContext.Menus.Where(x => x.Id == menu.ParentId).First();
                this._GetActiveMenu(menu);
            }
            return menu;
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
                menu.ParentId = menu.IsRoot ? null : menu.ParentId;

                //check if has role group
                if (request.RoleGroupIds.Count > 0)
                {
                    menu.RoleGroups = new HashSet<Data.Entities.RoleGroup>();

                    foreach (int roleGroupId in request.RoleGroupIds)
                    {
                        var roleGroup = DataContext.RoleGroups.Where(r => r.Id == roleGroupId).First();
                        
                        //add selected role group to menu
                        menu.RoleGroups.Add(roleGroup);  
                    }
                }
                else
                {
                    menu.RoleGroups = null;
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
                var item = DataContext.Entry(menu);

                item.State = EntityState.Modified;
                //Load RoleGroups Collection
                item.Collection("RoleGroups").Load();

                //set IsRoot if no menu as parent
                menu.IsRoot = request.ParentId <= 0;
                menu.ParentId = menu.IsRoot ? null : menu.ParentId;
                //clear RoleGroups collection first
                menu.RoleGroups.Clear();

                if (request.RoleGroupIds.Count > 0)
                {
                    //menu.RoleGroups = new HashSet<Data.Entities.RoleGroup>();

                    foreach (int roleGroupId in request.RoleGroupIds)
                    {
                        var roleGroup = DataContext.RoleGroups.Find(roleGroupId);

                        //add selected role group to menu
                        menu.RoleGroups.Add(roleGroup);
                    }
                }

                
                //DataContext.Menus.Attach(menu);
                //DataContext.Entry(menu).State = EntityState.Modified;
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
