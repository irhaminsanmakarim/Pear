using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class ArtifactDesignerViewModel
    {
        public ArtifactDesignerViewModel() {
            GraphicTypes = new List<SelectListItem>();
            Measurements = new List<SelectListItem>();
        }
        public string GraphicType { get; set; }
        public IList<SelectListItem> GraphicTypes { get; set; }
        public string GraphicName { get; set; }
        public string HeaderTitle { get; set; }

       
        public int MeasurementId { get; set; }
        public IList<SelectListItem> Measurements { get; set; }

        public BarChartViewModel BarChart { get; set; }
    }
}