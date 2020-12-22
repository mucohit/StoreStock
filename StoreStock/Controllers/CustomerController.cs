using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreStock.Models;
using PagedList;
using PagedList.Mvc;

namespace StoreStock.Controllers
{
    public class CustomerController : Controller
    {
        DbMVCStockEntities3 db = new DbMVCStockEntities3();
        // GET: Customer
        public ActionResult Index(int page =1)
        {
            var customers = db.tblCustomers.Where(c=>c.Situation == true).ToList().ToPagedList(page,3);

            return View(customers);
        }

        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCustomer(tblCustomer c)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            c.Situation = true;
            db.tblCustomers.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(tblCustomer c)
        {
            var customer = db.tblCustomers.Find(c.Id);
            customer.Situation = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = db.tblCustomers.Find(id);
            return View("Edit",customer);
        }

        [HttpPost]
        public ActionResult Edit(tblCustomer c) 
        {
            var customer = db.tblCustomers.Find(c.Id);
            customer.Name = c.Name;
            customer.LastName = c.LastName;
            customer.City = c.City;
            customer.Amount = c.Amount;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}