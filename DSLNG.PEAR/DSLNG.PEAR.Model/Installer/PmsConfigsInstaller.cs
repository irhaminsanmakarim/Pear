using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var scoreIndicatorSafety = new Collection<ScoreIndicator>
                {
                    new ScoreIndicator {Color = "red", Expression = "0 < x && x < 50"},
                    new ScoreIndicator {Color = "yellow", Expression = "50 <= x && x < 80"},
                    new ScoreIndicator {Color = "green", Expression = "80 <= x < 100"}
                };
            pmsConfigSafety.ScoreIndicators = scoreIndicatorSafety;

            pmsConfigSafety.ScoringType = ScoringType.Positive;
            pmsConfigSafety.Weight = 20;
            pmsConfigSafety.PmsSummary = _dataContext.PmsSummaries.Local.First(x => x.Id == 1);

            var pmsConfigProductivity = new PmsConfig();
            pmsConfigProductivity.Id = 2;
            pmsConfigProductivity.IsActive = true;
            pmsConfigProductivity.Pillar = _dataContext.Pillars.Local.First(x => x.Id == 2);
            var scoreIndicatorProductivity = new Collection<ScoreIndicator>
                {
                    new ScoreIndicator {Color = "red", Expression = "0 < x && x < 50"},
                    new ScoreIndicator {Color = "yellow", Expression = "50 <= x && x< 80"},
                    new ScoreIndicator {Color = "green", Expression = "80 <= x && x < 100"}
                };
            pmsConfigProductivity.ScoreIndicators = scoreIndicatorProductivity;
            pmsConfigProductivity.ScoringType = ScoringType.Positive;
            pmsConfigProductivity.Weight = 40;
            pmsConfigProductivity.PmsSummary = _dataContext.PmsSummaries.Local.First(x => x.Id == 1);
            
            _dataContext.PmsConfigs.AddOrUpdate(pmsConfigSafety);
            _dataContext.PmsConfigs.AddOrUpdate(pmsConfigProductivity);
        }
    }
}
