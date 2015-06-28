using System.Data.Entity.Migrations;
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
                Expression = "2 < x < 20"
            };

            var scoreIndicator2 = new ScoreIndicator
            {
                Id = 1,
                Color = "#eee",
                Expression = "2 < x < 20"
            };
            _dataContext.ScoreIndicators.AddOrUpdate(scoreIndicator1);
            _dataContext.ScoreIndicators.AddOrUpdate(scoreIndicator2);
        }
    }
}
