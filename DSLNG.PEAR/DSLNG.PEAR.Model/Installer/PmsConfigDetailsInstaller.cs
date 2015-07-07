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
            var pmsFatalityStrap = new PmsConfigDetails();
            pmsFatalityStrap.Id = 1;
            pmsFatalityStrap.AsGraphic = true;
            pmsFatalityStrap.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsFatalityStrap.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsFatalityStrap.IsActive = true;
            pmsFatalityStrap.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 1);
            pmsFatalityStrap.ScoringType = ScoringType.Boolean;
            pmsFatalityStrap.Weight = 50;
            pmsFatalityStrap.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);
            pmsFatalityStrap.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x == 0" });
            pmsFatalityStrap.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x == 50" });

            var pmsQHSE = new PmsConfigDetails();
            pmsQHSE.Id = 2;
            pmsQHSE.AsGraphic = true;
            pmsQHSE.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsQHSE.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsQHSE.IsActive = true;
            pmsQHSE.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 34);
            pmsQHSE.ScoringType = ScoringType.Positive;
            pmsQHSE.Weight = 30;
            pmsQHSE.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);
            pmsQHSE.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 50" });
            pmsQHSE.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "50 <= x && x <= 65" });
            pmsQHSE.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x > 65" });

            var pmsRIF = new PmsConfigDetails();
            pmsRIF.Id = 3;
            pmsRIF.AsGraphic = true;
            pmsRIF.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsRIF.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsRIF.IsActive = true;
            pmsRIF.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 35);
            pmsRIF.ScoringType = ScoringType.Negative;
            pmsRIF.Weight = 20;
            pmsRIF.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);
            pmsRIF.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 0.60" });
            pmsRIF.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "0.60 <= x && x > 0.50" });
            pmsRIF.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x <= 0.50" });


            var pmsPA = new PmsConfigDetails();
            pmsPA.Id = 4;
            pmsPA.AsGraphic = true;
            pmsPA.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsPA.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsPA.IsActive = true;
            pmsPA.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 10);
            pmsPA.ScoringType = ScoringType.Positive;
            pmsPA.Weight = 25;
            pmsPA.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);
            pmsPA.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 75" });
            pmsPA.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "75 <= x && x < 85" });
            pmsPA.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x >= 85" });

            var pmsPR = new PmsConfigDetails();
            pmsPR.Id = 5;
            pmsPR.AsGraphic = true;
            pmsPR.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsPR.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsPR.IsActive = true;
            pmsPR.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 12);
            pmsPR.ScoringType = ScoringType.Positive;
            pmsPR.Weight = 10;
            pmsPR.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);
            pmsPR.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 85" });
            pmsPR.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "85 <= x && x < 97" });
            pmsPR.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x >= 97" });

            var pmsThermal = new PmsConfigDetails();
            pmsThermal.Id = 6;
            pmsThermal.AsGraphic = true;
            pmsThermal.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsThermal.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsThermal.IsActive = true;
            pmsThermal.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 9);
            pmsThermal.ScoringType = ScoringType.Positive;
            pmsThermal.Weight = 15;
            pmsThermal.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);
            pmsThermal.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 80" });
            pmsThermal.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "80 <= x && x < 88" });
            pmsThermal.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x >= 88" });

            var pmsFlarred = new PmsConfigDetails();
            pmsFlarred.Id = 7;
            pmsFlarred.AsGraphic = true;
            pmsFlarred.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsFlarred.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsFlarred.IsActive = true;
            pmsFlarred.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 7);
            pmsFlarred.ScoringType = ScoringType.Negative;
            pmsFlarred.Weight = 25;
            pmsFlarred.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);
            pmsFlarred.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x >= 2.20" });
            pmsFlarred.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "2.20 > x && x >= 2.00" });
            pmsFlarred.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x < 2.00" });

            var pmsCargo = new PmsConfigDetails();
            pmsCargo.Id = 8;
            pmsCargo.AsGraphic = true;
            pmsCargo.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsCargo.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsCargo.IsActive = true;
            pmsCargo.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 14);
            pmsCargo.ScoringType = ScoringType.Positive;
            pmsCargo.Weight = 25;
            pmsCargo.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);
            pmsCargo.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 6" });
            pmsCargo.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x >= 6" });

            var pmsProduction = new PmsConfigDetails();
            pmsProduction.Id = 9;
            pmsProduction.AsGraphic = true;
            pmsProduction.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsProduction.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsProduction.IsActive = true;
            pmsProduction.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 22);
            pmsProduction.ScoringType = ScoringType.Negative;
            pmsProduction.Weight = 50;
            pmsProduction.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 3);
            pmsProduction.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 0.90" });
            pmsProduction.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "0.90 <= x && x >= 0.70" });
            pmsProduction.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x < 0.70" });


            var pmsBudgetUtil = new PmsConfigDetails();
            pmsBudgetUtil.Id = 10;
            pmsBudgetUtil.AsGraphic = true;
            pmsBudgetUtil.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsBudgetUtil.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsBudgetUtil.IsActive = true;
            pmsBudgetUtil.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 19);
            pmsBudgetUtil.ScoringType = ScoringType.Positive;
            pmsBudgetUtil.Weight = 50;
            pmsBudgetUtil.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 3);
            pmsBudgetUtil.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 95" });
            pmsBudgetUtil.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "x <= 95 && x >= 85" });
            pmsBudgetUtil.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x < 85" });

            var pmsEmployee = new PmsConfigDetails();
            pmsEmployee.Id = 1;
            pmsEmployee.AsGraphic = true;
            pmsEmployee.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsEmployee.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsEmployee.IsActive = true;
            pmsEmployee.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 31);
            pmsEmployee.ScoringType = ScoringType.Negative;
            pmsEmployee.Weight = 50;
            pmsEmployee.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 4);
            pmsEmployee.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 20" });
            pmsEmployee.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "20 <= x && x >= 15" });
            pmsEmployee.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x < 15" });


            var pmsCommunityAcceptance = new PmsConfigDetails();
            pmsCommunityAcceptance.Id = 1;
            pmsCommunityAcceptance.AsGraphic = true;
            pmsCommunityAcceptance.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsCommunityAcceptance.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            pmsCommunityAcceptance.IsActive = true;
            pmsCommunityAcceptance.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 25);
            pmsCommunityAcceptance.ScoringType = ScoringType.Positive;
            pmsCommunityAcceptance.Weight = 55;
            pmsCommunityAcceptance.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 4);
            pmsCommunityAcceptance.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 60" });
            pmsCommunityAcceptance.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "75 <= x && x >= 60" });
            pmsCommunityAcceptance.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x >= 75" });

            /* old */

            //var pmsConfigDetails1 = new PmsConfigDetails();
            //pmsConfigDetails1.Id = 1;
            //pmsConfigDetails1.AsGraphic = true;
            //pmsConfigDetails1.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails1.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails1.IsActive = true;
            //pmsConfigDetails1.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 19);
            //pmsConfigDetails1.ScoringType = ScoringType.Boolean;
            //pmsConfigDetails1.Weight = 50;
            //pmsConfigDetails1.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);
            //pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator {Color = "red", Expression = "x == 0"});
            //pmsConfigDetails1.ScoreIndicators.Add(new ScoreIndicator {Color = "green", Expression = "x == 50"});

            //var pmsConfigDetails2 = new PmsConfigDetails();
            //pmsConfigDetails2.Id = 2;
            //pmsConfigDetails2.AsGraphic = true;
            //pmsConfigDetails2.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails2.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails2.IsActive = true;
            //pmsConfigDetails2.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 2);
            //pmsConfigDetails2.ScoringType = ScoringType.Positive;
            //pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 50"});
            //pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "50 <= x && x <= 65" });
            //pmsConfigDetails2.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x > 65" });
            //pmsConfigDetails2.Weight = 30;
            //pmsConfigDetails2.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            //var pmsConfigDetails3 = new PmsConfigDetails();
            //pmsConfigDetails3.Id = 3;
            //pmsConfigDetails3.AsGraphic = true;
            //pmsConfigDetails3.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails3.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails3.IsActive = true;
            //pmsConfigDetails3.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 3);
            //pmsConfigDetails3.ScoringType = ScoringType.Negative;
            //pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x > 0.60"});
            //pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "0.60 <= x && x > 0.50"});
            //pmsConfigDetails3.ScoreIndicators.Add(new ScoreIndicator { Color = "green", Expression = "x <= 0.50" });
            //pmsConfigDetails3.Weight = 20;
            //pmsConfigDetails3.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            //var pmsConfigDetails4 = new PmsConfigDetails();
            //pmsConfigDetails4.Id = 4;
            //pmsConfigDetails4.AsGraphic = true;
            //pmsConfigDetails4.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails4.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails4.IsActive = true;
            //pmsConfigDetails4.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 4);
            //pmsConfigDetails4.ScoringType = ScoringType.Positive;
            //pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 75"});
            //pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "75 <= x && x < 85" });
            //pmsConfigDetails4.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x >= 85" });
            //pmsConfigDetails4.Weight = 25;
            //pmsConfigDetails4.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 1);

            //var pmsConfigDetails5 = new PmsConfigDetails();
            //pmsConfigDetails5.Id = 5;
            //pmsConfigDetails5.AsGraphic = true;
            //pmsConfigDetails5.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails5.UpdatedBy = _dataContext.Users.Local.First(x => x.Id == 1);
            //pmsConfigDetails5.IsActive = true;
            //pmsConfigDetails5.Kpi = _dataContext.Kpis.Local.First(x => x.Id == 5);
            //pmsConfigDetails5.ScoringType = ScoringType.Positive;
            //pmsConfigDetails5.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x < 75" });
            //pmsConfigDetails5.ScoreIndicators.Add(new ScoreIndicator { Color = "yellow", Expression = "75 <= x && x< 85" });
            //pmsConfigDetails5.ScoreIndicators.Add(new ScoreIndicator { Color = "red", Expression = "x >= 85" });
            //pmsConfigDetails5.Weight = 25;
            //pmsConfigDetails5.PmsConfig = _dataContext.PmsConfigs.Local.First(x => x.Id == 2);

            _dataContext.PmsConfigDetails.AddOrUpdate(pmsFatalityStrap);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsQHSE);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsRIF);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsPA);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsPR);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsThermal);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsFlarred);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsCargo);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsProduction);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsBudgetUtil);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsEmployee);
            _dataContext.PmsConfigDetails.AddOrUpdate(pmsCommunityAcceptance);
        }
    }
}
