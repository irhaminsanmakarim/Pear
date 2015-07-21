using DSLNG.PEAR.Data.Persistence;
using System.Linq;
using System.Data.Entity;
using DSLNG.PEAR.Data.Entities;

namespace DSLNG.PEAR.Data.Installer
{
    public class RoleGroupsInstaller
    {
        private DataContext _context;
        public RoleGroupsInstaller(DataContext context)
        {
            _context = context;
        }

        public void Install() {
            var groupPlanningDirectorate = new RoleGroup
            {
                Id = 1,
                Name = "Planning Directorate",
                Code = "CPD",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupOperationDirectorate = new RoleGroup
            {
                Id = 2,
                Name = "Operation Directorate",
                Code ="COO",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupFinanceDirectorate = new RoleGroup
            {
                Id = 3,
                Name = "Finance Directorate",
                Code = "CFD",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupTechnicalDirectorate = new RoleGroup
            {
                Id = 4,
                Name = "Technical Directorate",
                Code = "CTD",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupCommercialDirectorate = new RoleGroup
            {
                Id = 5,
                Name = "Commercial Directorate",
                Code = "CCD",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupCAffairDirectorate = new RoleGroup
            {
                Id = 6,
                Name = "Corporate Affair Directorate",
                Code = "CCD",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupPresdir = new RoleGroup
            {
                Id = 7,
                Name = "President Director Office",
                Code = "PDO",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            var groupHCMDirectorate = new RoleGroup
            {
                Id = 8,
                Name = "Human Capital Management",
                Code = "HCM",
                Level = _context.Levels.Local.Where(x => x.Id == 2).FirstOrDefault(),
                IsActive = true
            };
            var groupProcurement = new RoleGroup
            {
                Id = 9,
                Name = "Procurement",
                Code = "PRO",
                Level = _context.Levels.Local.Where(x => x.Id == 1).FirstOrDefault(),
                IsActive = true
            };
            var groupQHSE = new RoleGroup
            {
                Id = 10,
                Name = "QHSE",
                Code = "QHSE",
                Level = _context.Levels.Local.Where(x => x.Id == 2).FirstOrDefault(),
                IsActive = true
            };
            var groupIT = new RoleGroup
            {
                Id = 11,
                Name = "Information Communication & Tech",
                Code = "ICT",
                Level = _context.Levels.Local.Where(x => x.Id == 2).FirstOrDefault(),
                IsActive = true
            };
            var groupCSR = new RoleGroup
            {
                Id = 12,
                Name = "Community Support & Relation",
                Code = "CSR",
                Level = _context.Levels.Local.Where(x => x.Id == 2).FirstOrDefault(),
                IsActive = true
            };
            _context.RoleGroups.Add(groupPlanningDirectorate);
            _context.RoleGroups.Add(groupOperationDirectorate);
            _context.RoleGroups.Add(groupFinanceDirectorate);
            _context.RoleGroups.Add(groupTechnicalDirectorate);
            _context.RoleGroups.Add(groupCommercialDirectorate);
            _context.RoleGroups.Add(groupCAffairDirectorate);
            _context.RoleGroups.Add(groupPresdir);
            _context.RoleGroups.Add(groupHCMDirectorate);
            _context.RoleGroups.Add(groupProcurement);
            _context.RoleGroups.Add(groupQHSE);
            _context.RoleGroups.Add(groupIT);
            _context.RoleGroups.Add(groupCSR);
        }
    }
}
