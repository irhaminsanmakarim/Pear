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
            pmsConfigDetails1.ScoringType = ScoringType.Positive;
            pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator { Color = "#897879", IsActive = true, MaxValue = 20, MinValue = 0 });
            pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator { Color = "#897879", IsActive = true, MaxValue = 60, MinValue = 21 });
            pmsConfigDetails1.Weight = 100;
            pmsConfigDetails1.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            var pmsConfigDetails2 = new PmsConfigDetails();
            pmsConfigDetails2.Id = 1;
            pmsConfigDetails2.AsGraphic = true;
            pmsConfigDetails2.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails2.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsConfigDetails2.IsActive = true;
            pmsConfigDetails2.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 2);
            pmsConfigDetails2.ScoringType = ScoringType.Positive;
            pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "#897879", IsActive = true, MaxValue = 20, MinValue = 0 });
            pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "#897879", IsActive = true, MaxValue = 60, MinValue = 21 });
            pmsConfigDetails2.Weight = 100;
            pmsConfigDetails2.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);


            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails1);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsConfigDetails2);
        }
    }
}
