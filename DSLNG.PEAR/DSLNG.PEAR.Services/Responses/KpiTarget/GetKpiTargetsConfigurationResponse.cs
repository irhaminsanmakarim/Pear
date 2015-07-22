using System;
using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Responses.KpiTarget
{
    public class GetKpiTargetsConfigurationResponse : BaseResponse
    {
        public GetKpiTargetsConfigurationResponse()
        {
            Kpis = new List<Kpi>();   
        }

        public IList<Kpi> Kpis { get; set; }
        public string RoleGroupName { get; set; }
        public int RoleGroupId { get; set; }

        public class Kpi
        {
            public Kpi()
            {
                KpiTargets = new List<KpiTarget>();
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string PeriodeType { get; set; }
            public string Measurement { get; set; }
            public IList<KpiTarget> KpiTargets { get; set; }
        }

        public class KpiTarget
        {
            public int Id { get; set; }
            public string Remark { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
        }
    }
}
