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
    public class ActivityInstaller
    {
        private readonly DataContext _dataContext;
        public ActivityInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var activity = new Activity
            {
                Id = 1,
                Order = 1,
                IsActive = true,
                Remark = "-"
            };
            _dataContext.Activities.AddOrUpdate(activity);
        }
    }
}
