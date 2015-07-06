using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.KpiTarget
{
    public class UpdateKpiTargetViewModel 
    {
        public UpdateKpiTargetViewModel()
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
        }
    }
}