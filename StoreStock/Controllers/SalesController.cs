using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreStock.Models;


namespace StoreStock.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        DbMVCStockEntities3 db = new DbMVCStockEntities3();
        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.tblSales.ToList();
            return View(sales);
        }

        [HttpGet]
        public ActionResult NewSale()
        {
            //Products
            List<SelectListItem> product = (from p in db.tblProducts.ToList()
                                            select new SelectListItem
                                            {
                                                Text = p.Name ,
                                                Value = p.Id.ToString()
                                                
                                            }).ToList();
            ViewBag.prod = product;


            //Employees
            List<SelectListItem> emp = (from e in db.tblEmployees.ToList()
                                         select new SelectListItem
                                         {
                                             Text = e.Name + " "+e.LastName,
                                             Value = e.Id.ToString()
                                         }).ToList();

            ViewBag.emp = emp;

            //Customers
            List<SelectListItem> customers = (from c in db.tblCustomers.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = c.Name + " " + c.LastName,
                                                  Value = c.Id.ToString()
                                              }).ToList();

            ViewBag.cust = customers;

            return View();
        }

        [HttpPost]
        public ActionResult NewSale(tblSale sale)
        {
            var prdc = db.tblProducts.Where(p => p.Id == sale.tblProduct.Id).FirstOrDefault();
            var cust = db.tblCustomers.Where(c => c.Id == sale.tblCustomer.Id).FirstOrDefault();
            var emp = db.tblEmployees.Where(e => e.Id == sale.tblEmployee.Id).FirstOrDefault();

            sale.tblCustomer = cust;
            sale.tblEmployee = emp;
            sale.tblProduct = prdc;
            sale.Date =DateTime.Parse(DateTime.Now.ToShortDateString());
            db.tblSales.Add(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}