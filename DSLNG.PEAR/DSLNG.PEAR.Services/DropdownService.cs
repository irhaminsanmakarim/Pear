﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
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

        public IEnumerable<Dropdown> GetKpisForPmsConfigDetails(int pmsConfigId)
        {
            var pmsConfig = DataContext.PmsConfigs.Include(x => x.Pillar)
                                       .Include(x => x.PmsConfigDetailsList.Select(y => y.Kpi))
                                       .Single(x => x.Id == pmsConfigId);
            var kpiIds = pmsConfig.PmsConfigDetailsList.Select(x => x.Kpi.Id);

            return DataContext.Kpis.Where(x => x.Type.Code.ToLower() == Constants.Type.Corporate && x.Pillar.Id == pmsConfig.Pillar.Id 
                && !kpiIds.Contains(x.Id))
                .Select(x => new Dropdown
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }

        public IEnumerable<Dropdown> GetYearsForPmsSummary()
        {
            return DataContext.PmsSummaries.Select(x => new Dropdown
                {
                    Text = x.Year.ToString(),
                    Value = x.Year.ToString()
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
            for (int i = 0; i <= 30; i++)
            {
                years.Add(DateTime.Now.AddYears(i).Year);
            }
            return years.Select(x => new Dropdown
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
        }


        public IEnumerable<Dropdown> GetLevels()
        {
            return DataContext.Levels.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public IEnumerable<Dropdown> GetRoleGroups()
        {
            return DataContext.RoleGroups.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public IEnumerable<Dropdown> GetTypes()
        {
            return DataContext.Types.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public IEnumerable<Dropdown> GetGroups()
        {
            return DataContext.Groups.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public IEnumerable<Dropdown> GetMethods()
        {
            return DataContext.Methods.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        public IEnumerable<Dropdown> GetMeasurement()
        {
            return DataContext.Measurements.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public IEnumerable<Dropdown> GetYtdFormulas()
        {
            var ytd = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.YtdFormula)).Cast<DSLNG.PEAR.Data.Enums.YtdFormula>();
            return ytd.Select(x => new Dropdown { Text = x.ToString(), Value = x.ToString() }).ToList();
        }

        public IEnumerable<Dropdown> GetPeriodeTypes()
        {
            var periode = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.PeriodeType)).Cast<DSLNG.PEAR.Data.Enums.PeriodeType>();
            var ytd = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.YtdFormula)).Cast<DSLNG.PEAR.Data.Enums.YtdFormula>();
            return periode.Select(x => new Dropdown { Text = x.ToString(), Value = x.ToString() }).ToList();
        }


        public IEnumerable<Dropdown> GetKpis()
        {
            return DataContext.Kpis.Select(x => new Dropdown
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        public IEnumerable<Dropdown> GetPeriodeTypesForKpiTargetAndAchievement()
        {
            return new List<Dropdown>()
                {
                    new Dropdown {Text = PeriodeType.Monthly.ToString(), Value = PeriodeType.Monthly.ToString()},
                    new Dropdown {Text = PeriodeType.Yearly.ToString(), Value = PeriodeType.Yearly.ToString()}
                };
        }

        public IEnumerable<Dropdown> GetKpisForPmsConfigDetailsUpdate(int pmsConfigId, int id)
        {
             var pmsConfig = DataContext.PmsConfigs.Include(x => x.Pillar)
                                       .Include(x => x.PmsConfigDetailsList.Select(y => y.Kpi))
                                       .Single(x => x.Id == pmsConfigId);
            var kpiIds = pmsConfig.PmsConfigDetailsList.Where(x => x.Kpi.Id != id).Select(x => x.Kpi.Id);

            return DataContext.Kpis.Where(x => x.Type.Code.ToLower() == Constants.Type.Corporate && x.Pillar.Id == pmsConfig.Pillar.Id 
                && !kpiIds.Contains(x.Id))
                .Select(x => new Dropdown
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }


        public IEnumerable<Dropdown> GetConfigTypes()
        {
            var config = Enum.GetValues(typeof(DSLNG.PEAR.Data.Enums.ConfigType)).Cast<DSLNG.PEAR.Data.Enums.ConfigType>();
            return config.Select(x => new Dropdown { Text = x.ToString(), Value = x.ToString() }).ToList();
        }
    }
}
