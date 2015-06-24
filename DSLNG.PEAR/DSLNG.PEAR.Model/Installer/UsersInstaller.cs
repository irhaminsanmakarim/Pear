
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Data.Installer
{
    public class UsersInstaller
    {
        private DataContext _context;
        public UsersInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install() {

            var admin = new User
            {
                Id = 1,
                Email = "admin@regawa.com",
                IsActive = true,
                Username = "admin",
                Role = _context.RoleGroups.Local.Where(x => x.Id == 1).First()
            };

            _context.Users.Add(admin);
        }
    }
}
