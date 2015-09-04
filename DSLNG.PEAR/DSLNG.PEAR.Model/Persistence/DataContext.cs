﻿using System.Data.Entity;
using DSLNG.PEAR.Data.Entities;
using Type = DSLNG.PEAR.Data.Entities.Type;

namespace DSLNG.PEAR.Data.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
        }


        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<Artifact> Artifacts { get; set; }
        public IDbSet<ArtifactSerie> ArtifactSeries { get; set; }
        public IDbSet<ArtifactStack> ArtifactStacks { get; set; }
        public IDbSet<ArtifactPlot> ArtifactPlots { get; set; }
        public IDbSet<ArtifactRow> ArtifactRows { get; set; }
        public IDbSet<ArtifactChart> ArtifactCharts { get; set; }
        public IDbSet<Conversion> Conversions { get; set; }
        public IDbSet<DashboardTemplate> DashboardTemplates { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Kpi> Kpis { get; set; }
        public IDbSet<KpiAchievement> KpiAchievements { get; set; }
        public IDbSet<KpiTarget> KpiTargets { get; set; }
        public IDbSet<LayoutColumn> LayoutColumns { get; set; }
        public IDbSet<LayoutRow> LayoutRows { get; set; }
        public IDbSet<Level> Levels { get; set; }
        public IDbSet<Measurement> Measurements { get; set; }
        public IDbSet<Menu> Menus { get; set; }
        public IDbSet<Method> Methods { get; set; }
        public IDbSet<Periode> Periodes { get; set; }
        public IDbSet<Pillar> Pillars { get; set; }
        public IDbSet<PmsConfig> PmsConfigs { get; set; }
        public IDbSet<PmsConfigDetails> PmsConfigDetails { get; set; }
        public IDbSet<PmsSummary> PmsSummaries { get; set; }
        public IDbSet<RoleGroup> RoleGroups { get; set; }
        public IDbSet<ScoreIndicator> ScoreIndicators { get; set; }
        public IDbSet<Type> Types { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<KpiRelationModel> KpiRelationModels { get; set; }
        public IDbSet<ArtifactTank> ArtifactTanks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kpi>()
                        .HasMany(x => x.RelationModels)
                        .WithRequired(x => x.KpiParent);

            modelBuilder.Entity<PmsConfig>()
                .HasMany(x => x.PmsConfigDetailsList)
                .WithOptional(x => x.PmsConfig)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PmsSummary>()
                .HasMany(x => x.PmsConfigs)
                .WithOptional(x => x.PmsSummary)
                .WillCascadeOnDelete();

            //modelBuilder.Entity<Menu>()
            //    .HasKey(x => x.Id)
            //    .HasOptional(x => x.Parent)
            //    .WithMany()
            //    .HasForeignKey(x => x.ParentId);

            //modelBuilder.Entity<RoleGroup>()
            //    .HasMany(x => x.Menus)
            //    .WithMany(x=>x.RoleGroups)
            //    .Map( x =>
            //        {
            //            x.ToTable("MenusRoleGroups");
            //            x.MapLeftKey("MenuId");
            //            x.MapRightKey("ParentId");
            //        }
            //    );

            base.OnModelCreating(modelBuilder);
        }
        //public DbEntries 

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(x => x.Role)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }*/
    }
}
