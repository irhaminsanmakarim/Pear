using System.Collections.Generic;



namespace DSLNG.PEAR.Web.ViewModels.PmsSummary
{
    public class PmsSummaryConfigurationViewModel
    {
        public IEnumerable<CorporatePortofolio> CorporatePortofolios { get; set; }

        public class CorporatePortofolio
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Year { get; set; }
            public bool IsActive { get; set; }     
        }
        
    }
}