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
    public class PillarsInstaller
    {
        private readonly DataContext _dataContext;
        public PillarsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Installer()
        {
            var pilar1 = new Pillar();
            pilar1.Id = 1;
            pilar1.Code = "sft";
            pilar1.Name = "Safety";
            pilar1.Color = "#122381";
            pilar1.Order = 1;
            pilar1.Icon = "apalah";
            pilar1.Remark = "Ini Safety";
            pilar1.IsActive = true;

            var pilar2 = new Pillar();
            pilar2.Id = 2;
            pilar2.Code = "PaC";
            pilar2.Name = "Productivity and Efficienty";
            pilar2.Color = "#122381";
            pilar2.Order = 1;
            pilar2.Icon = "apalah";
            pilar2.Remark = "Ini Pac";
            pilar2.IsActive = true;

            _dataContext.Pilars.AddOrUpdate(pilar1);
            _dataContext.Pilars.AddOrUpdate(pilar2);
        }
    }
}
