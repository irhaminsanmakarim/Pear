
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Data.Installer
{
    public class UsersInstaller
    {
        private PasswordHasher _pass = new PasswordHasher();
        private SimpleCrypto.PBKDF2 crypto = new SimpleCrypto.PBKDF2();
        private DataContext _context;
        public UsersInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install()
        {

            //var admin = new User
            //{
            //    Id = 1,
            //    Email = "admin@regawa.com",
            //    Password = _pass.HashPassword("admin@regawa.com"),
            //    IsActive = true,
            //    Username = "admin",
            //    Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault()
            //};

            //_context.Users.Add(admin);
            /*
            var admin1 = new User { Id = 1, Email = "admin@dslng.com", Password = _pass.HashPassword("admin@dslng.com"), IsActive = true, Username = "Admin", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin2 = new User { Id = 2, Email = "donggi1@dslng.com", Password = _pass.HashPassword("donggi1@dslng.com"), IsActive = true, Username = "Admin1", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin3 = new User { Id = 3, Email = "com@dslng.com", Password = _pass.HashPassword("com@dslng.com"), IsActive = true, Username = "commercial", Role = _context.RoleGroups.Local.Where(x => x.Id == 5).FirstOrDefault() };
            var admin4 = new User { Id = 4, Email = "coo@dslng.com", Password = _pass.HashPassword("coo@dslng.com"), IsActive = true, Username = "operation", Role = _context.RoleGroups.Local.Where(x => x.Id == 2).FirstOrDefault() };
            var admin5 = new User { Id = 5, Email = "cfd@dslng.com", Password = _pass.HashPassword("cfd@dslng.com"), IsActive = true, Username = "finance", Role = _context.RoleGroups.Local.Where(x => x.Id == 3).FirstOrDefault() };
            var admin6 = new User { Id = 6, Email = "cpd@dslng.com", Password = _pass.HashPassword("cpd@dslng.com"), IsActive = true, Username = "Corporate Planning", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin7 = new User { Id = 7, Email = "qhse@dslng.com", Password = _pass.HashPassword("qhse@dslng.com"), IsActive = true, Username = "QHSE", Role = _context.RoleGroups.Local.Where(x => x.Id == 10).FirstOrDefault() };
            */

            var admin1 = new User { Id = 1, Email = "admin@dslng.com", Password = crypto.Compute("admin@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "Admin", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin2 = new User { Id = 2, Email = "donggi1@dslng.com", Password = crypto.Compute("donggi1@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "Admin1", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin3 = new User { Id = 3, Email = "com@dslng.com", Password = crypto.Compute("com@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "commercial", Role = _context.RoleGroups.Local.Where(x => x.Id == 5).FirstOrDefault() };
            var admin4 = new User { Id = 4, Email = "coo@dslng.com", Password = crypto.Compute("coo@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "operation", Role = _context.RoleGroups.Local.Where(x => x.Id == 2).FirstOrDefault() };
            var admin5 = new User { Id = 5, Email = "cfd@dslng.com", Password = crypto.Compute("cfd@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "finance", Role = _context.RoleGroups.Local.Where(x => x.Id == 3).FirstOrDefault() };
            var admin6 = new User { Id = 6, Email = "cpd@dslng.com", Password = crypto.Compute("cpd@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "Corporate Planning", Role = _context.RoleGroups.Local.Where(x => x.Id == 1).FirstOrDefault() };
            var admin7 = new User { Id = 7, Email = "qhse@dslng.com", Password = crypto.Compute("qhse@dslng.com"), PasswordSalt = crypto.Salt, IsActive = true, Username = "QHSE", Role = _context.RoleGroups.Local.Where(x => x.Id == 10).FirstOrDefault() };
            _context.Users.Add(admin1);
            _context.Users.Add(admin2);
            _context.Users.Add(admin3);
            _context.Users.Add(admin4);
            _context.Users.Add(admin5);
            _context.Users.Add(admin6);
            _context.Users.Add(admin7);


        }
    }
}
