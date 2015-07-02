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
            var mainmenu1 = new Menu { IsRoot = true, IsActive = true, Name = "Home", Module = "Home" };
            var mainmenu2 = new Menu { IsRoot = true, IsActive = true, Name = "PMS", Module = "PMS" };
            var mainmenu3 = new Menu { IsRoot = true, IsActive = true, Name = "Level", Module = "Level" };

            _dataContext.Menus.AddOrUpdate(mainmenu1);
            _dataContext.Menus.AddOrUpdate(mainmenu2);
            _dataContext.Menus.AddOrUpdate(mainmenu3);
        }
    }
}
