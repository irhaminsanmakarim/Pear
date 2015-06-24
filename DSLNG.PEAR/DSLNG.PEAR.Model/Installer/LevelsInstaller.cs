using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class LevelsInstaller
    {
        private DataContext _context;
        public LevelsInstaller(DataContext context) {
            _context = context;
        }
        public void Install() {
            var directorateLevel = new Level { Id = 1, Code = "DIR", Name = "Directorate", IsActive = true, Number = 1 };
            var corporateLevel = new Level { Id = 2, Code = "COR", Name = "Corporate", IsActive = true, Number = 2 };
            var functionLevel = new Level { Id = 3, Code = "FNC", Name = "Function", IsActive = true, Number = 3 };

            _context.Levels.Add(directorateLevel);
            _context.Levels.Add(corporateLevel);
            _context.Levels.Add(functionLevel);
        }
    }
}
