using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.ArtifactDesigner
{
    public class ArtifactDesignerViewModel
    {
        public string GraphicType { get; set; }
        public string GraphicName { get; set; }
        public string HeaderTitle { get; set; }
        public IEnumerable<SelectListItem> GraphicTypes { get; set; }
    }
}