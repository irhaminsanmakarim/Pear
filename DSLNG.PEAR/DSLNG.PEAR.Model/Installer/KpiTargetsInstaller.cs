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

        public void Install() {
            for (var i = 1; i <= 12; i++)
            {
                var kpiTarget = new KpiTarget
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = new DateTime(2015, i, 1),
                    PeriodeType = PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = 10 * i,
                    Kpi = _context.Kpis.Local.First(x => x.Id == 1)
                };
                _context.KpiTargets.Add(kpiTarget);
            }
            for (var i = 1; i <= 12; i++)
            {
                var kpiTarget = new KpiTarget
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.First(x => x.Id == 1),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = new DateTime(2015, i, 1),
                    PeriodeType = PeriodeType.Monthly,
                    Remark = "Whatever men...",
                    Value = 10 * i,
                    Kpi = _context.Kpis.Local.First(x => x.Id == 2)
                };
                _context.KpiTargets.Add(kpiTarget);
            }
          
        }
    }
}
