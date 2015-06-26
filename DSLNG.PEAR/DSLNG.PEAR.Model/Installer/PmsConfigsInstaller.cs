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
            var pmsConfigSafety = new PmsConfig();
            pmsConfigSafety.Id = 1;
            pmsConfigSafety.IsActive = true;
            pmsConfigSafety.Pillar = _dataContext.Pillars.Local.First(x => x.Id == 1);
            pmsConfigSafety.ScoreIndicators.Add(new ScoreIndicator
                {
                    IsActive = true,
                    Color = "#126712",
                    MaxValue = 20,
                    MinValue = 0
                });
            pmsConfigSafety.ScoringType = ScoringType.Positive;
            pmsConfigSafety.Weight = 80;
            pmsConfigSafety.PmsSummary = _dataContext.PmsSummaries.Local.First(x => x.Id == 1);

            var pmsConfigProductivity = new PmsConfig();
            pmsConfigProductivity.Id = 2;
            pmsConfigProductivity.IsActive = true;
            pmsConfigProductivity.Pillar = _dataContext.Pillars.Local.First(x => x.Id == 2);
            pmsConfigProductivity.ScoreIndicators.Add(new ScoreIndicator
            {
                IsActive = true,
                Color = "#126712",
                MaxValue = 20,
                MinValue = 0
            });
            pmsConfigProductivity.ScoringType = ScoringType.Positive;
            pmsConfigProductivity.Weight = 80;
            pmsConfigProductivity.PmsSummary = _dataContext.PmsSummaries.Local.First(x => x.Id == 1);
            
            _dataContext.PmsConfigs.AddOrUpdate(pmsConfigSafety);
            _dataContext.PmsConfigs.AddOrUpdate(pmsConfigProductivity);
        }
    }
}
