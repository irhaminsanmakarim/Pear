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
    public class GroupsInstaller
    {
        private readonly DataContext _dataContext;
        public GroupsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Installer()
        {
            var group = new Group();
            group.Id = 1;
            group.IsActive = true;
            group.Name = "Fatality";
            group.Order = 1;
            group.Remark = "test";

            var group2 = new Group();
            group2.Id = 2;
            group2.IsActive = true;
            group2.Name = "Security";
            group2.Order = 2;
            group2.Remark = "test";

            _dataContext.Groups.AddOrUpdate(group);
            _dataContext.Groups.AddOrUpdate(group2);
        }
    }
}
