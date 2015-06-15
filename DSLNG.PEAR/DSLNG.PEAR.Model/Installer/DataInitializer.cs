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
            var menus = new Menu { Id = 1, IsRoot = true, Module = "Home", Order = 0, Name = "Home", IsActive = true, Menus = null, RoleGroups = null };
            context.Menus.AddOrUpdate(menus);
            context.Users.AddOrUpdate(admin);
            context.SaveChanges();
        }
    }
}
