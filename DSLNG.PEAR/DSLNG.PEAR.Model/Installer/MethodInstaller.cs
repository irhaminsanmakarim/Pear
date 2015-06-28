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
            var method = new Method();
            method.Id = 1;
            method.Name = "Manual Input";
            method.IsActive = true;
            method.Remark = "hs";
            _dataContext.Methods.AddOrUpdate(method);
        }
    }
}
