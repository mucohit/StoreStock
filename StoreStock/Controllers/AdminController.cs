using StoreStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StoreStock.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        DbMVCStockEntities3 db = new DbMVCStockEntities3();


        [HttpGet]
        public ActionResult NewAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAdmin(tblAdmin a)
        {
            db.tblAdmins.Add(a);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}