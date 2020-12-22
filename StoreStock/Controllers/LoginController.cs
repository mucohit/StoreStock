using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreStock.Models;
using System.Web.Security;

namespace StoreStock.Controllers
{
    public class LoginController : Controller
    {
        DbMVCStockEntities3 db = new DbMVCStockEntities3();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblAdmin admin)
        {
            var info = db.tblAdmins.FirstOrDefault(a => a.userName == admin.userName && a.password == admin.password);
            if(info != null)
            {
                FormsAuthentication.SetAuthCookie(info.userName, false);
                return RedirectToAction("Index","Home");
            }
            else { 
             return View();
            }
        }

    }
}