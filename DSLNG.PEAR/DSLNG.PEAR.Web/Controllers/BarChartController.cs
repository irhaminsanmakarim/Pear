using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class BarChartController : Controller
    {
        //
        // GET: /BarChart/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BarChart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BarChart/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BarChart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BarChart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BarChart/Edit/5
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
        // GET: /BarChart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BarChart/Delete/5
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
