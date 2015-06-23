using System;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using Type = DSLNG.PEAR.Data.Entities.Type;

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

            AddUser(context);
            AddPilar(context);
            AddScoreIndicator(context);
            AddPmsData(context);
            AddGroup(context);
            AddMeasurement(context);
            AddConversion(context);
            AddMethod(context);
            AddPeriode(context);
            AddKpi(context);
            context.SaveChanges();
        }

        private void AddUser(DataContext context)
        {
            var user1 = new User();
            user1.Id = 9;
            user1.Username = "torres";
            user1.Email = "fernando@torres.com";
            user1.IsActive = true;
            user1.Role = new RoleGroup {Id = 1};
            context.Users.AddOrUpdate(user1);
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

        private void AddGroup(DataContext context)
        {
            var group = new Group();
            group.Id = 1;
            group.Activity = new Activity {Id = 1, IsActive = true, Order = 1, Remark = "remark"};
            group.IsActive = true;
            group.Name = "Fatality";
            group.Order = 1;
            group.Remark = "test";
            context.Groups.AddOrUpdate(group);
        }

        private void AddScoreIndicator(DataContext context)
        {
            var scoreIndicator1 = new ScoreIndicator();
            scoreIndicator1.Id = 1;
            scoreIndicator1.Color = "#897978";
            scoreIndicator1.MaxValue = 100;
            scoreIndicator1.MinValue = 10;
            scoreIndicator1.IsActive = true;
            scoreIndicator1.RefId = 1;
            context.ScoreIndicators.AddOrUpdate(scoreIndicator1);
        }

        private void AddMeasurement(DataContext context)
        {
            var measurement = new Measurement();
            measurement.Id = 1;
            measurement.IsActive = true;
            measurement.Name = "Measurement 1";
            measurement.Remark = "test";
            context.Measurements.AddOrUpdate(measurement);
        }

        private void AddConversion(DataContext context)
        {
            var conversion = new Conversion();
            conversion.Id = 1;
            conversion.From = new Measurement {Id = 1};
            conversion.To = new Measurement {Id = 1};
            conversion.IsReverse = false;
            conversion.Name = "Conversion One";
            conversion.Value = 200;
            context.Conversions.AddOrUpdate(conversion);
        }

        private void AddMethod(DataContext context)
        {
            var method = new Method();
            method.Id = 1;
            method.Name = "Manual Input";
            method.IsActive = true;
            method.Remark = "hs";
            context.Methods.AddOrUpdate(method);
        }
        /*private void AddType(DataContext context)
        {
            var type = new Type();
            level.Id = 1;
            level.Name = "Corporate Portofolio";
            level.Code = "cp";
            level.IsActive = true;
            level.Number = 1;
            level.Remark = "test";
            context.
        }*/

        private void AddPeriode(DataContext context)
        {
            var periode = new Periode();
            periode.Id = 1;
            periode.Name = PeriodeType.Monthly;
            periode.Remark = "Test";
            context.Periodes.AddOrUpdate(periode);
        }

        private void AddKpi(DataContext context)
        {
            var kpi = new Kpi();
            kpi.Id = 1;
            kpi.Pilar = new Pilar {Id = 1};
            kpi.Name = "Fatality/Strap Disability";
            kpi.Level = new Level {Id = 2};
            kpi.RoleGroup = new RoleGroup {Id = 1};
            kpi.Type = new Type {Id = 1};
            kpi.Group = new Group {Id = 1};
            kpi.Code = "ft";
            kpi.IsEconomic = false;
            kpi.Order = 1;
            kpi.YtdFormula = YtdFormula.Sum;
            kpi.Measurement = new Measurement {Id = 1};
            kpi.Method = new Method {Id = 1};
            kpi.Conversion = new Conversion {Id = 1};
            kpi.ConversionId = 1;
            kpi.FormatInput = FormatInput.Sum;
            kpi.Periode = new Periode {Id = 1};
            kpi.Value = DateTime.Now;
            context.Kpis.AddOrUpdate(kpi);
            


            //var relationModel1 = new KpiRelationModel();
            //relationModel1.Kpi
            //kpi.RelationModels.Add(new KpiRelationModel());
            //kpi.
            //kpi.

        }

        private void AddPmsData(DataContext context)
        {
            var pmsConfig = new PmsConfig();
            pmsConfig.Id = 1;
            pmsConfig.IsActive = true;
            pmsConfig.Pilar = new Pilar {Id = 1};
            pmsConfig.ScoreIndicator = new ScoreIndicator {Id = 1};
            pmsConfig.ScoringType = ScoringType.Positive;
            pmsConfig.Weight = 80;
            context.PmsConfigs.AddOrUpdate(pmsConfig);

            var pmsConfigDetail = new PmsConfigDetail();
            pmsConfigDetail.Id = 1;
            pmsConfigDetail.AsGraphic = true;
            pmsConfigDetail.CreatedBy = new User {Id = 9};
            pmsConfigDetail.UpdatedBy = new User {Id = 9};
            pmsConfigDetail.IsActive = true;
            pmsConfigDetail.Kpi = new Kpi{Id = 1};
            //pmsConfigDetail.KpiTargets.Add();

            

        }
    }
}
