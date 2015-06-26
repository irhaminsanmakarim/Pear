using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using System;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpiAchievementsInstaller
    {
        private readonly DataContext _context;
        public KpiAchievementsInstaller(DataContext context)
        {
            _context = context;
        }

        public void Install()
        {
            double?[] fatalityArr = new double?[] { 999, 0, 0, 0, 0, null, null, null, null, null, null, null, null, 0 };
            double?[] qhseTrainingAttendArr = new double?[]
                {999, 20, 10, 5, 5, 20, null, null, null, null, null, null, null, 99};
            double?[] plantAvailibiltyArr = new double?[] {999, null, null, null, null, null, null, null, null, null, null, null, null, 99};
            
            
            for (var i = 1; i <= 13; i++)
            {
                var kpiAchievement = new KpiAchievement
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = i == 13 ? new DateTime(2015, 1, 1) : new DateTime(2015, i, 1),
                    PeriodeType = i == 13 ? PeriodeType.Yearly : PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = fatalityArr[i],
                    Kpi = _context.Kpis.Local.First(x => x.Id == 1)
                };
                _context.KpiAchievements.Add(kpiAchievement);
            }

            for (var i = 1; i <= 13; i++)
            {
                var kpiAchievement = new KpiAchievement
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = i == 13 ? new DateTime(2015, 1, 1) : new DateTime(2015, i, 1),
                    PeriodeType = i == 13 ? PeriodeType.Yearly : PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = qhseTrainingAttendArr[i],
                    Kpi = _context.Kpis.Local.First(x => x.Id == 2)
                };
                _context.KpiAchievements.Add(kpiAchievement);
            }

            for (var i = 1; i <= 13; i++)
            {
                var kpiAchievement = new KpiAchievement
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = i == 13 ? new DateTime(2015, 1, 1) : new DateTime(2015, i, 1),
                    PeriodeType = i == 13 ? PeriodeType.Yearly : PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = plantAvailibiltyArr[i],
                    Kpi = _context.Kpis.Local.First(x => x.Id == 4)
                };
                _context.KpiAchievements.Add(kpiAchievement);
            }

        }

    }
}
