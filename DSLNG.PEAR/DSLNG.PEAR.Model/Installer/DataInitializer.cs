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
            
            //Measurements
            context.Measurements.AddOrUpdate(new Measurement { Name = "USD", IsActive = true });
            context.Measurements.AddOrUpdate(new Measurement { Name = "Number", IsActive = true });
            context.Measurements.AddOrUpdate(new Measurement { Name = "Times", IsActive = true });
            context.SaveChanges();
            //Kpi
            var measurement = new Measurement { Id = 1 };
            context.Kpis.AddOrUpdate(new Kpi { Code = "satu", Name = "Kpi Satu", Measurement = measurement });
            context.Kpis.AddOrUpdate(new Kpi { Code = "dua", Name = "Kpi Dua", Measurement = measurement });
            context.Kpis.AddOrUpdate(new Kpi { Code = "tiga", Name = "Kpi Tiga", Measurement = measurement });

            context.SaveChanges();
        }
    }
}
