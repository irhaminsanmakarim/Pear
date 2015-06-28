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
            group.Id = 2;
            group.IsActive = true;
            group.Name = "Security";
            group.Order = 2;
            group.Remark = "test";

            _dataContext.Groups.AddOrUpdate(group);
            _dataContext.Groups.AddOrUpdate(group2);
        }
    }
}
