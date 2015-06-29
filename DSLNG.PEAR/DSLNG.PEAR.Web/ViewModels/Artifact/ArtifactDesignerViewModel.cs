using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class ArtifactDesignerViewModel
    {
        public ArtifactDesignerViewModel() {
            GraphicTypes = new List<SelectListItem>();
            Measurements = new List<SelectListItem>();
        }
        [Display(Name= "Graphic Type")]
        public string GraphicType { get; set; }
        public IList<SelectListItem> GraphicTypes { get; set; }
        [Display(Name="Graphic Name")]
        public string GraphicName { get; set; }
        [Display(Name="Header Title")]
        public string HeaderTitle { get; set; }

        [Display(Name="Measurement")]
        public int MeasurementId { get; set; }
        public IList<SelectListItem> Measurements { get; set; }

        public BarChartViewModel BarChart { get; set; }
        public LineChartViewModel LineChart { get; set; }
    }
}