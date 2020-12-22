using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreStock.Models;

namespace StoreStock.Controllers
{
    public class CategoryController : Controller
    {
        DbMVCStockEntities3 db = new DbMVCStockEntities3();
        // GET: Category
        public ActionResult Index()
        {
            
            var categories = db.tblCategories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(tblCategory c)
        {
            db.tblCategories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var category = db.tblCategories.Find(id);
            db.tblCategories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id)
        {
            var category = db.tblCategories.Find(id);
            return View("GetCategory", category);
        }

        public ActionResult UpdateCategory(tblCategory k)
        {
            var category = db.tblCategories.Find(k.Id);
            category.Name = k.Name;
            
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}