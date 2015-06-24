using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class PmsConfigsInstaller
    {
        private readonly DataContext _dataContext;
        public PmsConfigsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var pmsConfig = new PmsConfig();
            pmsConfig.Id = 1;
            pmsConfig.IsActive = true;
            pmsConfig.Pillar = _dataContext.Pilars.Local.First(x => x.Id == 1);
            pmsConfig.ScoreIndicators.Add(new ScoreIndicator
                {
                    IsActive = true,
                    Color = "#126712",
                    MaxValue = 20,
                    MinValue = 0
                });
            pmsConfig.ScoringType = ScoringType.Positive;
            pmsConfig.Weight = 80;
            pmsConfig.PmsSummary = _dataContext.PmsSummaries.Local.First(x => x.Id == 1);
            
            _dataContext.PmsConfigs.AddOrUpdate(pmsConfig);
        }
    }
}
