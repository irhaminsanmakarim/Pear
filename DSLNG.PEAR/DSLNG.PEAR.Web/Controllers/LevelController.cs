using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Web.ViewModels.Level;
using System.Linq;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class LevelController : BaseController
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        //
        // GET: /Level/
        public ActionResult Index()
        {
            var dto = _levelService.GetLevels(new GetLevelsRequest());
            var model = dto.Levels.MapTo<LevelViewModel>();
            //var model = new LevelsViewModel() { Levels = dto.Levels.Select(x => new LevelViewModel { Id= x.Id, Code = x.Code, Name = x.Name, Number = x.Number, Remark = x.Remark, IsActive = x.IsActive }) };
            return View(model);
        }

        public ActionResult Create() {
            var viewModel = new LevelViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(LevelViewModel viewModel)
        { 
            var request  = viewModel.MapTo<CreateLevelRequest>();
            var response = _levelService.Create(request);
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
            var response = _levelService.GetLevel(new GetLevelRequest { Id = id });
            var viewModel = response.MapTo<LevelViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(LevelViewModel viewModel)
        {
            var request = viewModel.MapTo<UpdateLevelRequest>();
            var response = _levelService.Update(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View("Update", viewModel);
        }

        public ActionResult Delete(int id) {
            var response = _levelService.Delete(id);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            return RedirectToAction("Index");
        }

	}
}