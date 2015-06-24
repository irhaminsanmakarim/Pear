
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System.Linq;
using System.Data.Entity;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpisInstaller
    {
        private readonly DataContext _context;
        public KpisInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install() {
            var fatality = new Kpi
            {
                Id = 1,
                Name = "Fatality/Strap Disability",
                Measurement = _context.Measurements.Local.First(x => x.Id == 1),
                Pillar = _context.Pilars.Local.First(x => x.Id == 1)
            };
            var securityIncident = new Kpi
            {
                Id = 2,
                Name = "Security Incident",
                Measurement = _context.Measurements.Local.First(x => x.Id == 1),
                Pillar = _context.Pilars.Local.First(x => x.Id == 1)
            };
            _context.Kpis.Add(fatality);
            _context.Kpis.Add(securityIncident);
        }
    }
}
