using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.KpiAchievement
{
    public class AllKpiAchievementsResponse : BaseResponse
    {
        public AllKpiAchievementsResponse()
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
            public int Id { get; set; }
            public string Name { get; set; }
            public IList<Kpi> Kpis { get; set; }
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
