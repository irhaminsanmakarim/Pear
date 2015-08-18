using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Config
{
    public class GetConfigurationResponse : BaseResponse
    {
        public GetConfigurationResponse()
        {
            Kpis = new List<Kpi>();
        }

        public IList<Kpi> Kpis { get; set; }

        public class Kpi
        {
            public Kpi()
            {
                KpiAchievements = new List<KpiAchievement>();
                KpiTargets = new List<KpiTarget>();
                Economics = new List<Economic>();
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string PeriodeType { get; set; }
            public string Measurement { get; set; }
            public IList<KpiAchievement> KpiAchievements { get; set; }
            public IList<KpiTarget> KpiTargets { get; set; }
            public IList<Economic> Economics { get; set; }
        }

        public class KpiAchievement
        {
            public int Id { get; set; }
            public string Remark { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
        }

        public class KpiTarget
        {
            public int Id { get; set; }
            public string Remark { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
        }

        public class Economic
        {
            public int Id { get; set; }
            public string Remark { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
        }
    }
}
