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
            var hourly = new Periode { 
                Id = 1,
                Name = PeriodeType.Hourly
            };
            var daily = new Periode
            {
                Id = 1,
                Name = PeriodeType.Daily
            };
            var weekly = new Periode
            {
                Id = 1,
                Name = PeriodeType.Weekly
            };
            var monthly = new Periode
            {
                Id = 1,
                Name = PeriodeType.Monthly
            };
            var yearly = new Periode
            {
                Id = 1,
                Name = PeriodeType.Yearly
            };
            _dataContext.Periodes.AddOrUpdate(hourly);
            _dataContext.Periodes.AddOrUpdate(daily);
            _dataContext.Periodes.AddOrUpdate(weekly);
            _dataContext.Periodes.AddOrUpdate(monthly);
            _dataContext.Periodes.AddOrUpdate(yearly);

        }
    }
}
