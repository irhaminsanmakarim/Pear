using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace DSLNG.PEAR.Data.Installer
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);
            var directorateLevel = new Level { Id = 1, Code = "DIR", Name = "Directorate", IsActive = true, Number = 1 };
            var corporateLevel = new Level { Id = 2, Code = "COR", Name = "Corporate", IsActive = true, Number = 2 };
            var functionLevel = new Level { Id = 3, Code = "FNC", Name = "Function", IsActive = true, Number = 3 };


            var groupFinanceDirectorate = new RoleGroup
            {
                Id = 1,
                Name = "Finance Directorate",
                Level = directorateLevel,
                IsActive = true
            };

            var admin = new User
            {
                Id = 1,
                Email = "admin@regawa.com",
                IsActive = true,
                Username = "admin",
                Role = groupFinanceDirectorate
            };



            context.Levels.AddOrUpdate(directorateLevel);
            context.Levels.AddOrUpdate(corporateLevel);
            context.Levels.AddOrUpdate(functionLevel);
            context.RoleGroups.AddOrUpdate(groupFinanceDirectorate);
            var menu1 = new Menu { Id = 1, IsRoot = true, Module = "Home", Order = 0, Name = "Home", IsActive = true, Menus = null, RoleGroups = null };
            var menu2 = new Menu { Id = 2, IsRoot = true, Module = "PMS", Order = 1, Name = "PMS", IsActive = true, Menus = null, RoleGroups = null };
            var menu3 = new Menu { Id = 3, IsRoot = true, Module = "Level", Order = 0, Name = "Level", IsActive = true, Menus = null, RoleGroups = null };
            context.Menus.AddOrUpdate(menu1);
            context.Menus.AddOrUpdate(menu2);
            context.Menus.AddOrUpdate(menu3);
            context.Users.AddOrUpdate(admin);

            AddPilar(context);
            context.SaveChanges();
        }

        private void AddPilar(DataContext context)
        {
            var pilar1 = new Pilar();
            pilar1.Id = 1;
            pilar1.Code = "sft";
            pilar1.Name = "Safety";
            pilar1.Color = "#122381";
            pilar1.Order = 1;
            pilar1.Icon = "apalah";
            pilar1.Remark = "Ini Safety";
            pilar1.IsActive = true;

            var pilar2 = new Pilar();
            pilar1.Id = 2;
            pilar2.Code = "PaC";
            pilar2.Name = "Productivity and Efficienty";
            pilar2.Color = "#122381";
            pilar2.Order = 1;
            pilar2.Icon = "apalah";
            pilar2.Remark = "Ini Pac";
            pilar2.IsActive = true;

            context.Pilars.AddOrUpdate(pilar1);
            context.Pilars.AddOrUpdate(pilar2);
        }

        private void AddPmsData()
        {
            var pmsConfig = new PmsConfig();
            pmsConfig.IsActive = true;
            pmsConfig.Pilar = new Pilar {Id = 1};
        }
    }
}
