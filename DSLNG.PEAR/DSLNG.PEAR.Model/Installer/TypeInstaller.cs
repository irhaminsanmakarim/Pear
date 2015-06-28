using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class TypeInstaller
    {
        private DataContext _context;
        public TypeInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install()
        {
            var type1 = new Type { 
                Id = 1, 
                Name = "Corporate Portfolio",
                IsActive = true,
                Remark = "-"
            };
            var type2 = new Type
            {
                Id = 2,
                Name = "Performance Indicator",
                IsActive = true,
                Remark = "-"
            };
            var type3 = new Type
            {
                Id = 3,
                Name = "Indicator",
                IsActive = true,
                Remark = "-"
            };

            _context.Types.Add(type1);
            _context.Types.Add(type2);
            _context.Types.Add(type3);
        }
    }
}
