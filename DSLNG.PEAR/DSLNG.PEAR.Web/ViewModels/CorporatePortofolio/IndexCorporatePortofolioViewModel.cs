using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.CorporatePortofolio
{
    public class IndexCorporatePortofolioViewModel
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