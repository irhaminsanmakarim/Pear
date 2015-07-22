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
                Icon = "fa fa-dashboard"
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
                Icon = "fa fa-calendar"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu2);

            var performanceSetting = new Menu
            {
                Id = 15,
                IsRoot = false,
                IsActive = true,
                Name = "Performance Setting",
                Module = "Plan",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 2).First()
            };
            _dataContext.Menus.AddOrUpdate(performanceSetting);

            var corporatePortofolio = new Menu
            {
                Id = 16,
                IsRoot = false,
                IsActive = true,
                Name = "Corporate Portofolio",
                Module = "Plan",
                Url = "/PmsSummary/Configuration",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 15).First()
            };
            _dataContext.Menus.AddOrUpdate(corporatePortofolio);

            var pmsConfig = new Menu
            {
                Id = 20,
                IsRoot = false,
                IsActive = false,
                Name = "Add Pillar",
                Module = "Plan",
                Url = "/PmsConfig",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 16).First()
            };
            _dataContext.Menus.AddOrUpdate(pmsConfig);

            var pmsConfigDetail = new Menu
            {
                Id = 21,
                IsRoot = false,
                IsActive = false,
                Name = "Add KPI",
                Module = "Plan",
                Url = "/PmsConfigDetails",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 16).First()
            };
            _dataContext.Menus.AddOrUpdate(pmsConfigDetail);

            var kpiTarget = new Menu
            {
                Id = 18,
                IsRoot = false,
                IsActive = true,
                Name = "KPI Target",
                Module = "Plan",
                Url = "/kpitarget",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 15).First()
            };
            _dataContext.Menus.AddOrUpdate(kpiTarget);

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
                Icon = "fa fa-gavel"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu3);

            var kpiAchievement = new Menu
            {
                Id = 17,
                IsRoot = false,
                IsActive = true,
                Name = "KPI Achievement",
                Module = "Execute",
                Url = "/kpiachievement",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 3).First()
            };
            _dataContext.Menus.AddOrUpdate(kpiAchievement);

            

            var mainmenu4 = new Menu
            {
                Id = 4,
                IsRoot = true,
                IsActive = true,
                Name = "Assess",
                Module = "Assess",
                Icon = "fa fa-edit"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu4);

            var mainmenu5 = new Menu
            {
                Id = 5,
                IsRoot = true,
                IsActive = true,
                Name = "Report",
                Module = "Report",
                Icon = "fa fa-bar-chart-o"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu5);

            var mainmenu6 = new Menu
            {
                Id = 6,
                IsRoot = true,
                IsActive = true,
                Name = "Setting",
                Module = "Setting",
                Icon = "fa fa-cog"
            };
            _dataContext.Menus.AddOrUpdate(mainmenu6);

            var menuManagement = new Menu
            {
                Id = 11,
                IsRoot = false,
                IsActive = true,
                Name = "Menu Management",
                Module = "Setting",
                Url = "/Menu",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x=>x.Id==6).First()
            };
            _dataContext.Menus.AddOrUpdate(menuManagement);
            var createMenu = new Menu
            {
                Id = 18,
                IsRoot = false,
                IsActive = false,
                Name = "Create Menu",
                Module = "Execute",
                Url = "/Menu/Create",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 11).First()
            };
            _dataContext.Menus.AddOrUpdate(createMenu);

            var editMenu = new Menu
            {
                Id = 19,
                IsRoot = false,
                IsActive = false,
                Name = "Edit Menu",
                Module = "Execute",
                Url = "/Menu/Edit",
                RoleGroups = new List<RoleGroup>
                {
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 1).First(),
                    _dataContext.RoleGroups.Local.Where(x => x.Id == 2).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 11).First()
            };
            _dataContext.Menus.AddOrUpdate(editMenu);

            var userManagement = new Menu
            {
                Id = 12,
                IsRoot = false,
                IsActive = true,
                Name = "User Management",
                Module = "Setting",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 6).First()
            };
            _dataContext.Menus.AddOrUpdate(userManagement);

            var userOnly = new Menu
            {
                Id = 13,
                IsRoot = false,
                IsActive = true,
                Name = "User",
                Module = "Setting",
                Url = "/User",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 12).First()
            };
            _dataContext.Menus.AddOrUpdate(userOnly);

            var roleOnly = new Menu
            {
                Id = 14,
                IsRoot = false,
                IsActive = true,
                Name = "Role",
                Module = "Setting",
                Url = "/RoleGroup",
                RoleGroups = new List<RoleGroup>() { 
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 12).First()
            };
            _dataContext.Menus.AddOrUpdate(roleOnly);

            var submenu1_1 = new Menu
            {
                Id = 7,
                IsRoot = false,
                IsActive = true,
                Name = "PMS Summary",
                Module = "Dashboard",
                Url = "/PmsSummary",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==3).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==4).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==5).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==6).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==7).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==8).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==9).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==10).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==11).First(),
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==12).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id==1).First()
            };
            _dataContext.Menus.AddOrUpdate(submenu1_1);

            var configSetting = new Menu
            {
                Id = 8,
                IsRoot = false,
                IsActive = true,
                Name = "Configuration Settings",
                Module = "Dashboard",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 1).First()
            };
            _dataContext.Menus.AddOrUpdate(configSetting);

            var templateEditor = new Menu
            {
                Id = 9,
                IsRoot = false,
                IsActive = true,
                Name = "Template Editor",
                Module = "Dashboard",
                Url = "/Template",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 8).First()
            };
            _dataContext.Menus.AddOrUpdate(templateEditor);

            var artifactDesigner = new Menu
            {
                Id = 10,
                IsRoot = false,
                IsActive = true,
                Name = "Artifact Designer",
                Module = "Dashboard",
                Url = "/Artifact",
                RoleGroups = new List<RoleGroup>(){
                    _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First()
                },
                Parent = _dataContext.Menus.Local.Where(x => x.Id == 8).First()
            };
            _dataContext.Menus.AddOrUpdate(artifactDesigner);

            //var submenu1_2 = new Menu
            //{
            //    Id = 8,
            //    IsRoot = false,
            //    IsActive = true,
            //    Name = "Corporate Performance",
            //    Module = "Dashboard",
            //    RoleGroups = new List<RoleGroup>(){
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
            //    },
            //    Parent = _dataContext.Menus.Local.Where(x => x.Id == 1).First()
            //};
            //_dataContext.Menus.AddOrUpdate(submenu1_2);

            //var subsubmenu1_1_1 = new Menu
            //{
            //    Id = 9,
            //    IsRoot = false,
            //    IsActive = true,
            //    Name = "Productivity & Efficiency",
            //    Module = "Dashboard",
            //    RoleGroups = new List<RoleGroup>(){
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
            //    },
            //    Parent = _dataContext.Menus.Local.Where(x => x.Id == 8).First()
            //};
            //_dataContext.Menus.AddOrUpdate(subsubmenu1_1_1);

            //var subsubmenu1_1_2 = new Menu
            //{
            //    Id = 10,
            //    IsRoot = false,
            //    IsActive = true,
            //    Name = "Financial Strength",
            //    Module = "Dashboard",
            //    RoleGroups = new List<RoleGroup>(){
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
            //    },
            //    Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            //};
            //_dataContext.Menus.AddOrUpdate(subsubmenu1_1_2);

            //var subsubmenu1_1_3 = new Menu
            //{
            //    Id = 11,
            //    IsRoot = false,
            //    IsActive = true,
            //    Name = "Stakeholder Responsible",
            //    Module = "Dashboard",
            //    RoleGroups = new List<RoleGroup>(){
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
            //    },
            //    Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            //};
            //_dataContext.Menus.AddOrUpdate(subsubmenu1_1_3);

            //var subsubmenu1_1_4 = new Menu
            //{
            //    Id = 12,
            //    IsRoot = false,
            //    IsActive = true,
            //    Name = "Safety",
            //    Module = "Dashboard",
            //    RoleGroups = new List<RoleGroup>(){
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==1).First(),
            //        _dataContext.RoleGroups.Local.Where(x=>x.Id==2).First()
            //    },
            //    Parent = _dataContext.Menus.Local.Where(x => x.Id == 7).First()
            //};
            //_dataContext.Menus.AddOrUpdate(subsubmenu1_1_4);

            
        }
    }
}
