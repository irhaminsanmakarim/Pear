using System;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Web.ViewModels.KpiAchievement
{
    public class KpiAchievementsViewModel
    {
        public int Id { get; set; }
        public Kpi Kpi { get; set; }
        public double? Value { get; set; }
        public DateTime Periode { get; set; }
        public PeriodeType PeriodeType { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }

    public class Kpi
    {
        public int Id { get; set; }
        public Pillar Pillar { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
    }

    public class Pillar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}