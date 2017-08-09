using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjerTGR_PFE_2016_Fin.Models;

namespace ProjerTGR_PFE_2016_Fin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Default1/

      

        public ActionResult contact ()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult login(Users u)
        {
            if (ModelState.IsValid)
            {
                using (Base_Final_TGR2016Entities1 dc = new Base_Final_TGR2016Entities1())
                {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.password.Equals(u.password)).FirstOrDefault();
                    if (v != null)
                    {


                        Session["UserID"] = v.UserID.ToString();
                        Session["Username"] = v.Username.ToString();
                        return RedirectToAction("PrincipalePage");
                    }
                  
                }
            }
            
            return View(u);
        }

        public ActionResult PrincipalePage()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("login", "Home");
        }

    }
}
