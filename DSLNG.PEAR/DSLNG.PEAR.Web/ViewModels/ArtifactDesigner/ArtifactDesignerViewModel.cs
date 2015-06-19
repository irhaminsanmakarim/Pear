using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.ArtifactDesigner
{
    public class ArtifactDesignerViewModel
    {
        public ArtifactDesignerViewModel() {
            GraphicTypes = new List<SelectListItem>();
            SeriesTypes = new List<SelectListItem>();
        }
        public string GraphicType { get; set; }
        public IList<SelectListItem> GraphicTypes { get; set; }
        public string GraphicName { get; set; }
        public string HeaderTitle { get; set; }

        public IList<SelectListItem> SeriesTypes { get; set; }
       
    }
}