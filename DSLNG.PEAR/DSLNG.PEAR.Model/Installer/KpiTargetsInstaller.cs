using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using System;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpiTargetsInstaller
    {
        private readonly DataContext _context;
        public KpiTargetsInstaller(DataContext context)
        {
            _context = context;
        }

        public void Install()
        {
            double?[] fatalityArr = new double?[] { 999, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double?[] qhseTrainingAttendArr = new double?[] { 999, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 65 };
            double?[] rifArr = new double?[] { 999, null, null, null, null, null, null, null, null, null, null, null, 0.20, 0.50 };
            double?[] plantAvailibiltyArr = new double?[] {999, null, null, null, null, null, 85, 85, 85, 85, 85, 85, 85, 85};
            for (var i = 1; i <= 13; i++)
            {
                var kpiTarget = new KpiTarget
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
                _context.KpiTargets.Add(kpiTarget);
            }

            for (var i = 1; i <= 13; i++)
            {
                var kpiTarget = new KpiTarget
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
                _context.KpiTargets.Add(kpiTarget);
            }

            for (var i = 1; i <= 13; i++)
            {
                var kpiTarget = new KpiTarget
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = i == 13 ? new DateTime(2015, 1, 1) : new DateTime(2015, i, 1),
                    PeriodeType = i == 13 ? PeriodeType.Yearly : PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = rifArr[i],
                    Kpi = _context.Kpis.Local.First(x => x.Id == 3)
                };
                _context.KpiTargets.Add(kpiTarget);
            }

            for (var i = 1; i <= 13; i++)
            {
                var kpiTarget = new KpiTarget
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
                _context.KpiTargets.Add(kpiTarget);
            }

        }
    }
}
