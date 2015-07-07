
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Requests.Template;
using DSLNG.PEAR.Web.ViewModels.Template;
using DSLNG.PEAR.Common.Extensions;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class TemplateController : BaseController
    {
        public IArtifactService _artifactService;
        public ITemplateService _templateService;

        public TemplateController(IArtifactService artifactService, ITemplateService templateService) {
            _artifactService = artifactService;
            _templateService = templateService;
        }

        public ActionResult ArtifactList(string term)
        {
            var artifacts = _artifactService.GetArtifactsToSelect(new GetArtifactsToSelectRequest { Term = term }).Artifacts;
            return Json(new { results = artifacts }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Template/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Template/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Template/Create
        [HttpPost]
        public ActionResult Create(TemplateViewModel viewModel)
        {
            _templateService.CreateTemplate(viewModel.MapTo<CreateTemplateRequest>());
            return RedirectToAction("Index");
        }

        //
        // GET: /Template/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Template/Edit/5
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
        // GET: /Template/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Template/Delete/5
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
