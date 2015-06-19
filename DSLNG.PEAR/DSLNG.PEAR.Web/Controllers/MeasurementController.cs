using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using System;
using System.Linq;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Measurement;
using DSLNG.PEAR.Services.Responses.Measurement;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MeasurementController : BaseController
    {

        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        //
        // GET: /Measurement/
        public ActionResult Index()
        {
            var dto = _measurementService.GetMeasurements(new GetMeasurementsRequest()).Units.ToList();
            return View(dto);
        }

        public ActionResult Create()
        {
            var viewModel = new CreateMeasurementViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateMeasurementViewModel viewModel)
        {
            var request = viewModel.MapTo<CreateMeasurementRequest>();
            var response = _measurementService.Create(request);
            ViewBag.IsSuccess = response.IsSuccess;
            ViewBag.Message = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }

        public ActionResult Update(int id)
        {
            var response = _measurementService.GetMeasurement(new GetMeasurementRequest {Id = id});
            var viewModel = response.MapTo<UpdateMeasurementViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateMeasurementViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateMeasurementRequest>();
            var response = _measurementService.Update(request);
            ViewBag.IsSuccess = response.IsSuccess;
            ViewBag.Message = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Update", viewModel);
        }

        /*[ValidateInput(false)]
        public ActionResult MeasurementViewPartial()
        {
            var model = _service.GetMeasurements(new GetMeasurementsRequest()).Units.ToList();
            return PartialView("_MeasurementViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MeasurementViewPartialAddNew(GetMeasurementResponse item)
        {
            var model = item;
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
        }*/
    }
}