using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.KpiTarget
{
    public class UpdateKpiTargetRequest
    {
        public UpdateKpiTargetRequest()
        {
            Pillars = new List<Pillar>();
        }

        public IList<Pillar> Pillars { get; set; }

        public class Pillar
        {
            public Pillar()
            {
                Kpis = new List<Kpi>();
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public IList<Kpi> Kpis { get; set; }
        }

        public class Kpi
        {
            public Kpi()
            {
                KpiTargets = new List<KpiTarget>();
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Measurement { get; set; }
            public string Remark { get; set; }

            public IList<KpiTarget> KpiTargets { get; set; }
        }

        public class KpiTarget
        {
            public int Id { get; set; }
            public DateTime Periode { get; set; }
            public double? Value { get; set; }
            public string Remark { get; set; }
        }

        public string PeriodeType { get; set; }
    }
}
