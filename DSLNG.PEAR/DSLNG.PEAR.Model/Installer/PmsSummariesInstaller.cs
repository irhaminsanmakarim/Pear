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
    public class PmsSummariesInstaller
    {
        private readonly DataContext _dataContext;

        public PmsSummariesInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var pmsSummary = new PmsSummary();
            pmsSummary.CreatedBy = _dataContext.Users.Local.First(x => x.Id == 1);

            pmsSummary.Id = 1;
            pmsSummary.IsActive = true;
            pmsSummary.Year = 2015;
            pmsSummary.ScoreIndicators.Add(new ScoreIndicator()
            {
                IsActive = true,
                Color = "#213243",
                MaxValue = 100,
                MinValue = 20
            });
            pmsSummary.Title = "1st Operation Year";
            pmsSummary.CreatedDate = DateTime.Now;
            pmsSummary.UpdatedDate = DateTime.Now;
            _dataContext.PmsSummaries.AddOrUpdate(pmsSummary);
        }
    }
}
