using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Web.ViewModels.Level;
using System.Linq;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class LevelController : Controller
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
	}
}