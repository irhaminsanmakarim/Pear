using System.Collections.Generic;

namespace DSLNG.PEAR.Web.ViewModels.KpiAchievement
{
    public class IndexKpiAchievementViewModel
    {
        public IndexKpiAchievementViewModel()
        {
            RoleGroups = new List<RoleGroup>();
        }
        public IList<RoleGroup> RoleGroups { get; set; }

        public class RoleGroup
        {
            public RoleGroup()
            {
                Kpis = new List<Kpi>();
            }
            public string Name { get; set; }
            public IList<Kpi> Kpis { get; set; }
            public int Id { get; set; }
        }

        public class Kpi
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Measurement { get; set; }
            public string Type { get; set; }
        }
    }
}