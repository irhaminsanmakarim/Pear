using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    [AllowAnonymous]
    public class UnAuthorizedController : Controller
    {
        //
        // GET: /UnAuthorize/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error() {
            return View();
        }
	}
}