using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MeasurementController : BaseController
    {

        private readonly IMeasurementService _service;

        public MeasurementController(IMeasurementService service)
        {
            this._service = service;
        }

        //
        // GET: /Measurement/
        public ActionResult Index()
        {
            var dto = _service.GetMeasurements(new GetMeasurementsRequest()).Units.ToList();
            return View(dto);
        }

        [ValidateInput(false)]
        public ActionResult MeasurementViewPartial()
        {
            var model = _service.GetMeasurements(new GetMeasurementsRequest()).Units.ToList();
            return PartialView("_MeasurementViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MeasurementViewPartialAddNew(DSLNG.PEAR.Services.Responses.Measurement.GetMeasurementResponse item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MeasurementViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MeasurementViewPartialUpdate(DSLNG.PEAR.Services.Responses.Measurement.GetMeasurementResponse item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_MeasurementViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult MeasurementViewPartialDelete(System.Int32 Id)
        {
            var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_MeasurementViewPartial", model);
        }
	}
}