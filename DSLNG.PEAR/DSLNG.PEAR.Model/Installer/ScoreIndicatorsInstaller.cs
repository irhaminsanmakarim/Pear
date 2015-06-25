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
    public class ScoreIndicatorsInstaller
    {
        private readonly DataContext _dataContext;
        public ScoreIndicatorsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Install()
        {
            var scoreIndicator1 = new ScoreIndicator
            {
                Id = 1,
                Color = "#000",
                MinValue = 2,
                MaxValue = 20,
                IsActive = true
            };

            var scoreIndicator2 = new ScoreIndicator
            {
                Id = 1,
                Color = "#eee",
                MinValue = 2,
                MaxValue = 20,
                IsActive = true
            };
            _dataContext.ScoreIndicators.AddOrUpdate(scoreIndicator1);
            _dataContext.ScoreIndicators.AddOrUpdate(scoreIndicator2);
        }
    }
}
