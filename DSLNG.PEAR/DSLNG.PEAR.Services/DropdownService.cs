using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;

namespace DSLNG.PEAR.Services
{
    public class DropdownService : BaseService, IDropdownService
    {
        public DropdownService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<Dropdown> GetScoringTypes()
        {
            return new List<Dropdown>()
                {
                    new Dropdown {Text = ScoringType.Boolean.ToString(), Value = ScoringType.Boolean.ToString()},
                    new Dropdown {Text = ScoringType.Positive.ToString(), Value = ScoringType.Positive.ToString()},
                    new Dropdown {Text = ScoringType.Negative.ToString(), Value = ScoringType.Negative.ToString()}
                };
        }

        public IEnumerable<Dropdown> GetPillars()
        {
            return DataContext.Pillars.Select(x => new Dropdown
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }

        public IEnumerable<Dropdown> GetPillars(int pmsSummaryId)
        {
            var notAvailablePillars = DataContext.PmsConfigs
                                                 .Include(x => x.PmsSummary)
                                                 .Where(x => x.PmsSummary.Id == pmsSummaryId).Select(x => x.Pillar);

            return DataContext.Pillars.Except(notAvailablePillars).Select(x => new Dropdown
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }

        public IEnumerable<Dropdown> GetKpis(int pillarId)
        {
            return DataContext.Kpis.Include(x => x.Pillar).Where(x => x.Pillar.Id == pillarId)
                .Select(x => new Dropdown
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
        }

        public IEnumerable<Dropdown> GetMonths()
        {
            return DateTimeFormatInfo
                   .InvariantInfo
                   .MonthNames
                   .Where(m => !String.IsNullOrEmpty(m))
                   .Select((monthName, index) => new Dropdown()
                   {
                       Value = (index + 1).ToString(),
                       Text = monthName
                   });
        }

        public IEnumerable<Dropdown> GetYears()
        {
            var years = new List<int>();
            for (int i = 0; i <= 5; i++)
            {
                years.Add(DateTime.Now.AddYears(-i).Year);
            }
            return years.Select(x => new Dropdown
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
        }
    }
}
