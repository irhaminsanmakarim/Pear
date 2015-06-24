
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Web.ViewModels.Pillar;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class PilarController : BaseController
    {
        public IPillarService _pillarService;

        public PilarController(IPillarService pillarService)
        {
            _pillarService = pillarService;
        }
        //
        // GET: /Pilar/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Pilar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Pilar/Create
        public ActionResult Create()
        {
            var viewModel = new PillarViewModel();
            return View(viewModel);
        }

        //
        // POST: /Pilar/Create
        [HttpPost]
        public ActionResult Create(PillarViewModel viewModel)
        {

            // TODO: Add insert logic here
            _pillarService.Create(viewModel.MapTo<CreatePillarRequest>());
            return RedirectToAction("Create");

        }

        //
        // GET: /Pilar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Pilar/Edit/5
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
        // GET: /Pilar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Pilar/Delete/5
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
