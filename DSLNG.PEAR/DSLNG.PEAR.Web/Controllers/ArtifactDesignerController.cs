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
        //
        // GET: /ArtifactDesigner/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ArtifactDesigner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ArtifactDesigner/Create
        public ActionResult Create()
        {
            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
            viewModel.SeriesTypes.Add(new SelectListItem { Value = "single", Text = "Single Stack" });
            viewModel.SeriesTypes.Add(new SelectListItem { Value = "multiple", Text = "Multiple Stack" });
            return View(viewModel);
        }

        public ActionResult SeriesPartial()
        {
            if (Request.QueryString["series_type"] == "single")
            {
                return PartialView("_SeriesPartial");
            }
            else
            {
                return PartialView("_MultiStackSeriesPartial");
            }
        }
        public ActionResult KpiSeriesPartial()
        {
            return PartialView("_KpiSeriesPartial");
        }
        public ActionResult StackPartial()
        {
            return PartialView("_StackPartial");
        }

        //
        // POST: /ArtifactDesigner/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ArtifactDesigner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ArtifactDesigner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ArtifactDesigner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ArtifactDesigner/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
