using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class MenuInstaller
    {
        private readonly DataContext _dataContext;

        public MenuInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var roleGroup1 = _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First();
            var roleGroup2 = _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First();
            var list = new List<RoleGroup>()
                {
                    roleGroup1,
                    roleGroup2
                };

            var mainmenu1 = new Menu { 
                Id = 1, 
                IsRoot = true, 
                IsActive = true, 
                Name = "Dashboard", 
                Module = "Dashboard",
                RoleGroups = list,
                Icon = "<i class='fa fa-dashboard'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu1);

            var mainmenu2 = new Menu {
                Id = 2,
                IsRoot = true,
                IsActive = true,
                Name = "Plan",
                Module = "Plan",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Icon = "<i class='fa fa-calendar'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu2);

            var mainmenu3 = new Menu {
                Id = 3,
                IsRoot = true,
                IsActive = true,
                Name = "Execute",
                Module = "Execute",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Icon = "<i class='fa fa-gavel'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu3);

            var mainmenu4 = new Menu
            {
                Id = 4,
                IsRoot = true,
                IsActive = true,
                Name = "Assess",
                Module = "Assess",
                Icon = "<i class='fa fa-edit'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu4);

            var mainmenu5 = new Menu
            {
                Id = 5,
                IsRoot = true,
                IsActive = true,
                Name = "Report",
                Module = "Report",
                Icon = "<i class='fa fa-bar-chart-o'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu5);

            var mainmenu6 = new Menu
            {
                Id = 6,
                IsRoot = true,
                IsActive = true,
                Name = "Setting",
                Module = "Setting",
                Icon = "<i class='fa fa-cog'></i>"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu6);


            var submenu1_1 = new Menu
            {
                Id = 7,
                IsRoot = false,
                IsActive = true,
                Name = "PMS Summary",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id==1).First()
            };
            _dataContext.Menus.AddOrUpdate(submenu1_1);

            var subsubmenu1_1_1 = new Menu
            {
                Id = 9,
                IsRoot = false,
                IsActive = true,
                Name = "Productivity & Efficiency",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            };
            _dataContext.Menus.AddOrUpdate(subsubmenu1_1_1);

            var subsubmenu1_1_2 = new Menu
            {
                Id = 10,
                IsRoot = false,
                IsActive = true,
                Name = "Financial Strength",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            };
            _dataContext.Menus.AddOrUpdate(subsubmenu1_1_2);

            var subsubmenu1_1_3 = new Menu
            {
                Id = 11,
                IsRoot = false,
                IsActive = true,
                Name = "Stakeholder Responsible",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            };
            _dataContext.Menus.AddOrUpdate(subsubmenu1_1_3);

            var subsubmenu1_1_4 = new Menu
            {
                Id = 12,
                IsRoot = false,
                IsActive = true,
                Name = "Safety",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            };
            _dataContext.Menus.AddOrUpdate(subsubmenu1_1_4);

            var submenu1_2 = new Menu
            {
                Id = 8,
                IsRoot = false,
                IsActive = true,
                Name = "Corporate Performance",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 1).First()
            };
            _dataContext.Menus.AddOrUpdate(submenu1_2);
        }
    }
}
