using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Web.ViewModels.Menu;
using DSLNG.PEAR.Data.Entities;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MenuController : Controller
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        public PartialViewResult MainMenu() {
            return PartialView();
        }

        //
        // GET: /Menu/
        public ActionResult Index()
        {
            var dto = _menuService.GetMenus(new GetMenuRequest());
            var model = dto.Menus.MapTo<MenusViewModel>();
            return View(model);
        }

        //
        // GET: /Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Menu/Create
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
        // GET: /Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Menu/Edit/5
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
        // GET: /Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Menu/Delete/5
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
