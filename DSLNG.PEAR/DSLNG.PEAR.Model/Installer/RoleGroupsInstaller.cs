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
            var groupFinanceDirectorate = new RoleGroup
            {
                Id = 1,
                Name = "Finance Directorate",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            _context.RoleGroups.Add(groupFinanceDirectorate);

            var groupCorporateDirectorate = new RoleGroup
            {
                Id = 2,
                Name = "Corporate Directorate",
                Level = _context.Levels.Local.Where(x => x.Id == 1).First(),
                IsActive = true
            };
            _context.RoleGroups.Add(groupCorporateDirectorate);
        }
    }
}
