using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class MethodInstaller
    {
        private readonly DataContext _dataContext;
        public MethodInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var method1 = new Method
            {
                Id = 1,
                Name = "Formula",
                IsActive = true
            };
            var method2 = new Method {
                Id = 2,
                Name = "External Source",
                IsActive = true
            };
            var method3 = new Method {
                Id = 3,
                Name = "Manual Input"
            };
            _dataContext.Methods.AddOrUpdate(method1);
            _dataContext.Methods.AddOrUpdate(method2);
            _dataContext.Methods.AddOrUpdate(method3);
        }
    }
}
