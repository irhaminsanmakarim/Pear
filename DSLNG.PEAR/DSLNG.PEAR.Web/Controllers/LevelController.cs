using DSLNG.PEAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
	}
}