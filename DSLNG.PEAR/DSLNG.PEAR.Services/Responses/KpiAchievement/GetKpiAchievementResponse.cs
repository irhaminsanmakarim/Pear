using DSLNG.PEAR.Data.Enums;
using System;

namespace DSLNG.PEAR.Services.Responses.KpiAchievement
{
    public class GetKpiAchievementResponse : BaseResponse
    {
        public int Id { get; set; }
        public Kpi kpi { get; set; }
        public double? Value { get; set; }
        public DateTime Periode { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }

        public class Kpi
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Measurement { get; set; }
            public string Remark { get; set; }

        }
    }
}
