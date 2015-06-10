using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Model.Entities;
using Type = DSLNG.PEAR.Model.Entities.Type;

namespace DSLNG.PEAR.Model.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }


        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<Artifact> Artifacts { get; set; }
        public IDbSet<ArtifactSerie> ArtifactSeries { get; set; }
        public IDbSet<ArtifactStack> ArtifactStacks { get; set; }
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
        public IDbSet<Method> Methods { get; set; }
        public IDbSet<Periode> Periodes { get; set; }
        public IDbSet<Pilar> Pilars { get; set; }
        public IDbSet<PmsConfig> PmsConfigs { get; set; }
        public IDbSet<PmsConfigDetail> PmsConfigDetails { get; set; }
        public IDbSet<PmsSummary> PmsSummaries { get; set; }
        public IDbSet<RoleGroup> RoleGroups { get; set; }
        public IDbSet<ScoreIndicator> ScoreIndicators { get; set; }
        public IDbSet<Type> Types { get; set; }
        public IDbSet<User> Users { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(x => x.Role)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }*/
    }
}
