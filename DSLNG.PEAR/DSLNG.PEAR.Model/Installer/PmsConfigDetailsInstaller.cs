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
    public class PmsConfigDetailsInstaller
    {
        private readonly DataContext _dataContext;
        public PmsConfigDetailsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var pmsConfigDetails1 = new PmsConfigDetails();
            pmsConfigDetails1.Id = 1;
            pmsConfigDetails1.AsGraphic = true;
            pmsConfigDetails1.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails1.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails1.IsActive = true;
            pmsConfigDetails1.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 1);
            pmsConfigDetails1.ScoringType = ScoringType.Boolean;
            pmsConfigDetails1.Weight = 50;
            pmsConfigDetails1.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);
            pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator {Color = "red", Expression = "x > 0"});
            pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator {Color = "green", Expression = "x == 0"});

            var pmsConfigDetails2 = new PmsConfigDetails();
            pmsConfigDetails2.Id = 2;
            pmsConfigDetails2.AsGraphic = true;
            pmsConfigDetails2.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails2.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails2.IsActive = true;
            pmsConfigDetails2.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 2);
            pmsConfigDetails2.ScoringType = ScoringType.Positive;
            pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 50"});
            pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "50 <= x <= 65" });
            pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x > 65" });
            pmsConfigDetails2.Weight = 30;
            pmsConfigDetails2.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            var pmsConfigDetails3 = new PmsConfigDetails();
            pmsConfigDetails3.Id = 3;
            pmsConfigDetails3.AsGraphic = true;
            pmsConfigDetails3.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails3.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails3.IsActive = true;
            pmsConfigDetails3.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 3);
            pmsConfigDetails3.ScoringType = ScoringType.Negative;
            pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 0.60"});
            pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "0.60 <= x > 0.50"});
            pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x <= 0.50" });
            pmsConfigDetails3.Weight = 20;
            pmsConfigDetails3.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            var pmsConfigDetails4 = new PmsConfigDetails();
            pmsConfigDetails4.Id = 4;
            pmsConfigDetails4.AsGraphic = true;
            pmsConfigDetails4.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails4.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails4.IsActive = true;
            pmsConfigDetails4.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 4);
            pmsConfigDetails4.ScoringType = ScoringType.Positive;
            pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 75"});
            pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "75 <= x < 85"});
            pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x >= 85" });
            pmsConfigDetails4.Weight = 25;
            pmsConfigDetails4.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);

            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails1);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails2);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails3);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails4);
        }
    }
}
