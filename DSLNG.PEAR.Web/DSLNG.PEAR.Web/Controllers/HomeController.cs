using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG_PEAR_Web.Models;

namespace DSLNG_PEAR_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            
            return View();    
        }
        
        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView");
        }
    
    }
}