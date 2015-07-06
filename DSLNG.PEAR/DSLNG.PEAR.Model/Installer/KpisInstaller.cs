
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.ObjectModel;

namespace DSLNG.PEAR.Data.Installer
{
    public class KpisInstaller
    {
        private readonly DataContext _context;
        public KpisInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install()
        {
            //var kpi1 = new Kpi { Id = 1, Name = "Fatality/Strap disability", Measurement= _context.Measurements.Local.First(x => x.Id == 6), PillarId = 1
            //, Group = _context.Groups.Local.First(x=> x.Id==1), Order=1, YtdFormula = Enums.YtdFormula.Sum, IsEconomic= true, FormatInput = FormatInput.Numeric, IsActive = true, CreatedDate = DateTime.Parse("14/02/2014"), UpdatedDate = DateTime.Now
            //, Method = _context.Methods.Local.First(x=> x.Id==3), Level = _context.Levels.Local.First(x=>x.Id==3), RoleGroup= _context.RoleGroups.Local.First(x=>x.Id==10)};
            var fatality = new Kpi
            {
                Id = 1,
                Name = "Fatality/Strap Disability",
                Measurement = _context.Measurements.Local.First(x => x.Id == 6),
                Pillar = _context.Pillars.Local.First(x => x.Id == 1),
                Group = _context.Groups.Local.First(x => x.Id == 1),
                Order = 1,
                YtdFormula = Enums.YtdFormula.Sum,
                IsEconomic = true,
                FormatInput = FormatInput.Numeric,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Method = _context.Methods.Local.First(x => x.Id == 1),
                Level = _context.Levels.Local.First(x => x.Code == "COR")
            };
            //};
            //var fatality = new Kpi
            //{
            //    Id = 1,
            //    Name = "Fatality/Strap Disability",
            //    Measurement = _context.Measurements.Local.First(x => x.Id == 1),
            //    Pillar = _context.Pillars.Local.First(x => x.Id == 1),
            //    Group = _context.Groups.Local.First(x => x.Id == 1),
            //    Order = 1,
            //    YtdFormula = Enums.YtdFormula.Sum,
            //    IsEconomic = true,
            //    FormatInput = FormatInput.Numeric,
            //    IsActive = true,
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now,
            //    Method = _context.Methods.Local.First(x => x.Id == 1),
            //    Level = _context.Levels.Local.First(x => x.Code == "COR")
            //};
            //var securityIncident = new Kpi
            //{
            //    Id = 2,
            //    Name = "QHSE Training Attend",
                Measurement = _context.Measurements.Local.First(x => x.Id == 6),
            //    Pillar = _context.Pillars.Local.First(x => x.Id == 1),
            //    Group = _context.Groups.Local.First(x => x.Id == 1),
            //    Order = 2,
            //    YtdFormula = Enums.YtdFormula.Sum,
            //    IsEconomic = true,
            //    FormatInput = FormatInput.Numeric,
            //    IsActive = true,
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now,
            //    Level = _context.Levels.Local.First(x => x.Code == "COR")
            //};
            //var rif = new Kpi
            //{
            //    Id = 3,
            //    Name = "RIF",
                Measurement = _context.Measurements.Local.First(x => x.Id == 6),
            //    Pillar = _context.Pillars.Local.First(x => x.Id == 1),
            //    Group = _context.Groups.Local.First(x => x.Id == 1),
            //    Order = 3,
            //    YtdFormula = Enums.YtdFormula.Sum,
            //    IsEconomic = true,
            //    FormatInput = FormatInput.Numeric,
            //    IsActive = true,
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now,
            //    Level = _context.Levels.Local.First(x => x.Code == "COR")
            //};

            //var dafwc = new Kpi
            //{
            //    Id = 4,
            //    Name = "DAFWC",
                Measurement = _context.Measurements.Local.First(x => x.Id == 6),
                Pillar = _context.Pillars.Local.First(x => x.Id == 1),
                Group = _context.Groups.Local.First(x => x.Id == 1),
                Order = 4,
                YtdFormula = Enums.YtdFormula.Sum,
                IsEconomic = true,
                FormatInput = FormatInput.Numeric,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Level = _context.Levels.Local.First(x => x.Code == "COR")
            };

            var plantAvailability = new Kpi
            {
                Id = 5,
                Name = "Plant Availability",
                Measurement = _context.Measurements.Local.First(x => x.Id == 6),
                Pillar = _context.Pillars.Local.First(x => x.Id == 2),
                Order = 3,
                Level = _context.Levels.Local.First(x => x.Code == "COR")
            };

            _context.Kpis.Add(fatality);
            _context.Kpis.Add(kpi2);
            _context.Kpis.Add(kpi3);
            _context.Kpis.Add(kpi4);
            _context.Kpis.Add(kpi5);
            _context.Kpis.Add(kpi6);
            _context.Kpis.Add(kpi7);
            _context.Kpis.Add(kpi8);
            _context.Kpis.Add(kpi9);
            _context.Kpis.Add(kpi10);
            _context.Kpis.Add(kpi11);
            _context.Kpis.Add(kpi12);
            _context.Kpis.Add(kpi13);
            _context.Kpis.Add(kpi14);
            _context.Kpis.Add(kpi15);
            _context.Kpis.Add(kpi16);
            _context.Kpis.Add(kpi17);
            _context.Kpis.Add(kpi18);
            _context.Kpis.Add(kpi19);
            _context.Kpis.Add(kpi20);
            _context.Kpis.Add(kpi21);
            _context.Kpis.Add(kpi22);
            _context.Kpis.Add(kpi23);
            _context.Kpis.Add(kpi24);
            _context.Kpis.Add(kpi25);
            _context.Kpis.Add(kpi26);
            _context.Kpis.Add(kpi27);
            _context.Kpis.Add(kpi28);
            _context.Kpis.Add(kpi29);
            _context.Kpis.Add(kpi30);
            _context.Kpis.Add(kpi31);
            _context.Kpis.Add(kpi32);
            _context.Kpis.Add(kpi33);
            _context.Kpis.Add(kpi34);
            _context.Kpis.Add(kpi35);

        }
    }
}
