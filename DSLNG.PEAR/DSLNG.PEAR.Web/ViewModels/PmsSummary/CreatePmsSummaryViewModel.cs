using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Common;
using DSLNG.PEAR.Web.ViewModels.Common.PmsSummary;

namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class CreatePmsSummaryViewModel
    {
        public CreatePmsSummaryViewModel()
        {
            ScoreIndicators = new List<ScoreIndicatorViewModel>()
                {
                    new ScoreIndicatorViewModel()
                };
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        //public IEnumerable<SelectListItem> Years { get; set; }
        public IList<ScoreIndicatorViewModel> ScoreIndicators { get; set; }
        public bool IsActive { get; set; }
    }
}