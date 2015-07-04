using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Responses;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IDropdownService
    {
        IEnumerable<Dropdown> GetScoringTypes();
        IEnumerable<Dropdown> GetPillars();
        IEnumerable<Dropdown> GetPillars(int pmsSummaryId);
        IEnumerable<Dropdown> GetKpis(int pillarId);
        IEnumerable<Dropdown> GetYears();
        IEnumerable<Dropdown> GetMonths();
        IEnumerable<Dropdown> GetKpisForPmsConfigDetails(int pmsConfigId);
    }

    public class Dropdown
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }


}
