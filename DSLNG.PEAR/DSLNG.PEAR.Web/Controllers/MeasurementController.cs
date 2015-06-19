using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Measurement;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MeasurementController : BaseController
    {

        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        public ActionResult Index()
        {
            var response = _measurementService.GetMeasurements(new GetMeasurementsRequest());
            var viewModel = new IndexMeasurementViewModel();
            viewModel.Measurements = response.Measurements.MapTo<MeasurementViewModel>();
            return View(viewModel);
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
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
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
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Update", viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var response = _measurementService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }

        
    }
}