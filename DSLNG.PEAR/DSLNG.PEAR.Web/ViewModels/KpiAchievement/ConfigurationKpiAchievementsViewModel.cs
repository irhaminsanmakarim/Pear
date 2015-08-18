using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.KpiAchievement
{
    public class ConfigurationKpiAchievementsViewModel
    {
        public ConfigurationKpiAchievementsViewModel()
        {
            Kpis = new List<Kpi>();
        }
        public IList<Kpi> Kpis { get; set; }
        public string RoleGroupName { get; set; }
        public int RoleGroupId { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string PeriodeType { get; set; }
        public string FileName { get; set; }

        public class Kpi
        {
            public Kpi()
            {
                KpiAchievements = new List<KpiAchievement>();
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string PeriodeType { get; set; }
            public string Measurement { get; set; }
            public IList<KpiAchievement> KpiAchievements { get; set; }
        }
        
        public class KpiAchievement
        {
            public int Id { get; set; }
            public string Remark { get; set; }
            public double? Value { get; set; }
            public DateTime Periode { get; set; }
        }
    }
}