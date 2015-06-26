
using System.Collections.ObjectModel;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System.Linq;
using System.Data.Entity;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpiWithRelationModelInstaller
    {
        private readonly DataContext _context;
        public KpiWithRelationModelInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install()
        {
            var list = new Collection<KpiRelationModel>();
            var plantAvailability = _context.Kpis.Local.First(x => x.Id == 3);
            var item1 = new KpiRelationModel();
            item1.Kpi = plantAvailability;
            item1.Method = "Quantitative";
            list.Add(item1);
            
            var plantReliability = new Kpi
            {
                Id = 4,
                Name = "Plant Reliability",
                Measurement = _context.Measurements.Local.First(x => x.Id == 1),
                Pillar = _context.Pillars.Local.First(x => x.Id == 2),
                Order = 2,
                RelationModels = list
            };

            _context.Kpis.Add(plantReliability);
            
        }
    }
}
