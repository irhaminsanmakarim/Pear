using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsConfigDetails
{
    public class GetPmsConfigDetailsResponse : BaseResponse
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public KpiData GroupKpi { get; set; }
        public List<KpiAchievment> KpiAchievments { get; set; }
        public KpiAchievment KpiAchievmentYearly { get; set; }
        public List<KpiRelation> KpiRelations { get; set; }

        public class KpiData
        {
            public string Group { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
            public string Period { get; set; }
            public decimal ActualYearly { get; set; }
            public decimal ActualMonthly { get; set; }
        }

        public class KpiAchievment
        {
            public string Type { get; set; }
            public string Period { get; set; }
            public string Remark { get; set; }
        }

        public class KpiRelation
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public string RelationModel { get; set; }
            public decimal ActualYearly { get; set; }
            public decimal ActualMonthly { get; set; }
        }
    }
}
