using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreStock.Models;

namespace StoreStock.Controllers
{
    public class ProductController : Controller
    {
        DbMVCStockEntities3 db = new DbMVCStockEntities3();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.tblProducts.Where(p=>p.Situation==true).ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> ctg = (from c in db.tblCategories.ToList()
                                        select new SelectListItem
                                        {
                                            Text = c.Name,
                                            Value = c.Id.ToString()
                                        }).ToList();
            ViewBag.drop = ctg;
            List<SelectListItem> brand = (from b in db.tblProducts.ToList()
                                          select new SelectListItem
                                          {
                                              Text = b.Brand,
                                              Value = b.Id.ToString()
                                          }).ToList() ;
            
            ViewBag.brand = brand;

            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(tblProduct p)
        {
            var ctg = db.tblCategories.Where(c => c.Id == p.tblCategory.Id).FirstOrDefault();
            p.tblCategory = ctg;
            p.Situation = true;
            db.tblProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(tblProduct p)
        {
            var product =db.tblProducts.Find(p.Id);
            product.Situation = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<SelectListItem> product = (from p in db.tblCategories.ToList()
                                            select new SelectListItem
                                            {
                                                Text = p.Name,
                                                Value = p.Id.ToString()
                                            }).ToList();
            var prdc = db.tblProducts.Find(id);
            ViewBag.productCategory = product;
            return View("Edit",prdc);
        }

        [HttpPost]
        public ActionResult Edit(tblProduct p)
        {
            var product = db.tblProducts.Find(p.Id);
            product.Name = p.Name;
            product.PurchasePrice = p.PurchasePrice;
            product.SellPrice = p.SellPrice;
            product.Stock = p.Stock;
            product.Brand = p.Brand;

            var ctg = db.tblCategories.Where(x => x.Id == p.tblCategory.Id).FirstOrDefault();
            product.Category = ctg.Id;
           
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        

        
    }
}