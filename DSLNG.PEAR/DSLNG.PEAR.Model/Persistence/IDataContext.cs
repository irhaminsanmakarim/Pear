using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Model.Entities;

namespace DSLNG.PEAR.Model.Persistence
{
    public interface IDataContext
    {
        IDbSet<Activity> Activities { get; set; }
        IDbSet<Artifact> Artifacts { get; set; }
        IDbSet<ArtifactSerie> ArtifactSeries { get; set; }
        IDbSet<ArtifactStack> ArtifactStacks { get; set; }
        IDbSet<Conversion> Conversions { get; set; }
        IDbSet<DashboardTemplate> DashboardTemplates { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<Kpi> Kpis { get; set; }
        IDbSet<KpiAchievement> KpiAchievements { get; set; }
        IDbSet<KpiTarget> KpiTargets { get; set; }
        IDbSet<LayoutColumn> LayoutColumns { get; set; }
        IDbSet<LayoutRow> LayoutRows { get; set; }
        IDbSet<Level> Levels { get; set; }
        IDbSet<Measurement> Measurements { get; set; }
        IDbSet<Menu> Menus { get; set; }
        IDbSet<Method> Methods { get; set; }
        IDbSet<Periode> Periodes { get; set; }
        IDbSet<Pilar> Pilars { get; set; }
        IDbSet<PmsConfig> PmsConfigs { get; set; }
        IDbSet<PmsConfigDetail> PmsConfigDetails { get; set; }
        IDbSet<PmsSummary> PmsSummaries { get; set; }
        IDbSet<RoleGroup> RoleGroups { get; set; }
        IDbSet<ScoreIndicator> ScoreIndicators { get; set; }
        IDbSet<Entities.Type> Types { get; set; }
        IDbSet<User> Users { get; set; }

        Database Database { get; }
        int SaveChanges();
    }
}
