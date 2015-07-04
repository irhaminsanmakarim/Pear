


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class UpdatePmsSummaryViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IList<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
        public bool IsActive { get; set; }
    }
}