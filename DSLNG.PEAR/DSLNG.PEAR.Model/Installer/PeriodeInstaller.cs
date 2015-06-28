using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Data.Installer
{
    public class PeriodeInstaller
    {
        private readonly DataContext _dataContext;
        public PeriodeInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var periode = new Periode();
            periode.Id = 1;
            periode.Name = PeriodeType.Monthly;
            periode.Remark = "Test";
            _dataContext.Periodes.AddOrUpdate(periode);
        }
    }
}
