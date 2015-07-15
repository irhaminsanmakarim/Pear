
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
            var fsDAFWC = new KpiRelationModel {
                Id = 1,
                Kpi = _context.Kpis.Local.First(x=>x.Id == 2),
                KpiParent = _context.Kpis.Local.First(x=>x.Id == 1),
                Method = "Qualitative"
            };
            list.Add(fsDAFWC);
            var fsLOPC = new KpiRelationModel {
                Id = 2,
                KpiParent = _context.Kpis.Local.First(x=>x.Id ==1),
                Kpi = _context.Kpis.Local.First(x=>x.Id==3),
                Method = "Qualitative"
            };
            list.Add(fsLOPC);
            var fsSecIns = new KpiRelationModel {
                Id = 3,
                KpiParent = _context.Kpis.Local.First(x=>x.Id == 1),
                Kpi = _context.Kpis.Local.First(x=>x.Id == 4),
                Method = "Qualitative"
            };
            list.Add(fsSecIns);
            var fsQHSEAttend = new KpiRelationModel { 
                Id = 4,
                KpiParent = _context.Kpis.Local.First(x=>x.Id ==1),
                Kpi = _context.Kpis.Local.First(x=>x.Id == 34),
                Method = "Qualitative"
            };
            list.Add(fsQHSEAttend);
            var QHSEAttendFS = new KpiRelationModel
            {
                Id = 5,
                Kpi = _context.Kpis.Local.First(x => x.Id == 1),
                KpiParent = _context.Kpis.Local.First(x => x.Id == 34),
                Method = "Qualitative"
            };
            list.Add(QHSEAttendFS);
            var QHSEAttendLOPC = new KpiRelationModel
            {
                Id = 6,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 34),
                Kpi = _context.Kpis.Local.First(x => x.Id == 3),
                Method = "Qualitative"
            };
            list.Add(QHSEAttendLOPC);
            var QHSEAttendSecIns = new KpiRelationModel
            {
                Id = 7,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 34),
                Kpi = _context.Kpis.Local.First(x => x.Id == 4),
                Method = "Qualitative"
            };
            list.Add(QHSEAttendSecIns);
            var rifFS = new KpiRelationModel {
                Id = 8,
                KpiParent = _context.Kpis.Local.First(x=>x.Id == 35),
                Kpi = _context.Kpis.Local.First(x=>x.Id == 1),
                Method = "Qualitative"
            };
            list.Add(rifFS);
            var rifDAFWC = new KpiRelationModel { 
                Id = 9,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 35),
                Kpi = _context.Kpis.Local.First(x => x.Id == 2),
                Method = "Qualitative"
            };
            list.Add(rifDAFWC);
            var rifLOPC = new KpiRelationModel
            {
                Id = 10,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 35),
                Kpi = _context.Kpis.Local.First(x => x.Id == 3),
                Method = "Qualitative"
            };
            list.Add(rifLOPC);
            var rifSecIns = new KpiRelationModel
            {
                Id = 11,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 35),
                Kpi = _context.Kpis.Local.First(x => x.Id == 4),
                Method = "Qualitative"
            };
            list.Add(rifSecIns);
            int z = 11;

            z += 1;
            var planAvailFS = new KpiRelationModel {
                Id = z,
                KpiParent = _context.Kpis.Local.First(x=>x.Id == 10),
                Kpi = _context.Kpis.Local.First(x=>x.Id == 1),
                Method = "Qualitative"
            };
            list.Add(planAvailFS);
            z += 1;
            var planAvaiDAFWC = new KpiRelationModel
            {
                Id = z,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 10),
                Kpi = _context.Kpis.Local.First(x => x.Id == 2),
                Method = "Qualitative"
            };
            list.Add(planAvaiDAFWC);

            z += 1;
            var planAvaiSec = new KpiRelationModel
            {
                Id = z,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 10),
                Kpi = _context.Kpis.Local.First(x => x.Id == 4),
                Method = "Qualitative"
            };
            list.Add(planAvaiSec);

            z += 1;
            var planAvaiRel = new KpiRelationModel
            {
                Id = z,
                KpiParent = _context.Kpis.Local.First(x => x.Id == 10),
                Kpi = _context.Kpis.Local.First(x => x.Id == 12),
                Method = "Qualitative"
            };
            list.Add(planAvaiRel);

            /*
             * remarked temporary due to pillar check on pms summary
            var fsQHSEClosure = new KpiRelationModel {
                Id = 5,
                KpiParent = _context.Kpis.Local.First(x=>x.Id == 1),
                Kpi = _context.Kpis.Local.First(x=>x.Id == 0),
                Method = "Qualitative"
            };
            list.Add(fsQHSEClosure);
            */

            //var plantAvailability = _context.Kpis.Local.First(x => x.Id == 3);
            //var item1 = new KpiRelationModel();
            //item1.Kpi = plantAvailability;
            //item1.Method = "Quantitative";
            //list.Add(item1);
            
            //var plantReliability = new Kpi
            //{
            //    Id = 4,
            //    Name = "Plant Reliability",
            //    Measurement = _context.Measurements.Local.First(x => x.Id == 1),
            //    Pillar = _context.Pillars.Local.First(x => x.Id == 2),
            //    Order = 2,
            //    RelationModels = list
            //};

            //_context.Kpis.Add(plantReliability);
            
        }
    }
}
