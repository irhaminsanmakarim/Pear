using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpiTargetsInstaller
    {
        private DataContext _context;
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
                    CreatedBy = _context.Users.Local.Where(x => x.Id == 1).First(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = new DateTime(2015, i, 1),
                    Remark = "Whatever men...",
                    Value = 10 * i,
                    Kpi = _context.Kpis.Local.Where(x => x.Id == 1).First()
                };
                _context.KpiTargets.Add(kpiTarget);
            }
            for (var i = 1; i <= 12; i++)
            {
                var kpiTarget = new KpiTarget
                {
                    Id = 1,
                    CreatedBy = _context.Users.Local.Where(x => x.Id == 1).First(),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Periode = new DateTime(2015, i, 1),
                    Remark = "Whatever men...",
                    Value = 10 * i,
                    Kpi = _context.Kpis.Local.Where(x => x.Id == 2).First()
                };
                _context.KpiTargets.Add(kpiTarget);
            }
          
        }
    }
}
