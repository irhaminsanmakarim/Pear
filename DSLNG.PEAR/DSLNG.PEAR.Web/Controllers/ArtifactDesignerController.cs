using DSLNG.PEAR.Web.ViewModels.ArtifactDesigner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ArtifactDesignerController : BaseController
    {
        public ActionResult Create()
        {
            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
            viewModel.SeriesTypes.Add(new SelectListItem { Value = "single", Text = "Single Stack" });
            viewModel.SeriesTypes.Add(new SelectListItem { Value = "multiple", Text = "Multiple Stack" });
            return View(viewModel);
        }
    }
}
