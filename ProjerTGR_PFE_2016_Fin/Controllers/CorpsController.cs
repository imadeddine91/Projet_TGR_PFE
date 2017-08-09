using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjerTGR_PFE_2016_Fin.Models;
using PagedList;

namespace ProjerTGR_PFE_2016_Fin.Controllers
{
    public class CorpsController : Controller
    {
        private Base_Final_TGR2016Entities1 db = new Base_Final_TGR2016Entities1();

        //
        // GET: /Corps/

        public void listSelect()
        {
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg");
           

        }

        public ActionResult Index(int page = 1, int pagesize = 2)
        {

            if (TempData["error"] == "add")
            {
                listSelect();
                List<Corps> listegrad = db.Corps.ToList();
                PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                ViewData["messag"] = "add";
                return View(model);

            }
            else if (TempData["error"] == "edit")
            {
                listSelect();
                List<Corps> listegrad = db.Corps.ToList();
                PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                ViewData["messag"] = "edit";
                return View(model);

            }
            else if (TempData["error"] == "delete")
            {
                listSelect();
                List<Corps> listegrad = db.Corps.ToList();
                PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                ViewData["messag"] = "delete";
                return View(model);

            }
            else if (TempData["error"] == "error")
            {
                listSelect();
                List<Corps> listegrad = db.Corps.ToList();
                PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                ViewData["messag"] = "error";
                return View(model);

            }
            else if (TempData["error"] == "existe")
            {
                listSelect();
                List<Corps> listegrad = db.Corps.ToList();
                PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                ViewData["messag"] = "existe";
                return View(model);

            }

            listSelect();
            List<Corps> listegradd = db.Corps.ToList();
            PagedList<Corps> modell = new PagedList<Corps>(listegradd, page, pagesize);
            return View(modell);




        

      
        }

         public ActionResult serach (int id_catg=0, int strSearch=0,int page = 1, int pagesize = 2)
        {
             if(id_catg !=0 || strSearch !=0)
             {
                 var corp = from b in db.Corps
                            select b;
                 if (id_catg == 0 && strSearch == 0)
                 {
                     listSelect();
                     List<Corps> listegrad = corp.ToList();
                     PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                     return View("Index", model);
                    

                 }
                 else if (id_catg != 0 && strSearch == 0)
                 {
                     corp = corp.Where(m => m.id_catg == id_catg);
                     listSelect();
                     List<Corps> listegrad = corp.ToList();
                     PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                     return View("Index", model);


                     
                 }
                 else if (id_catg != 0 && strSearch != 0)
                 {
                     corp = corp.Where(m => m.id_catg == id_catg && m.Num_corp == strSearch);
                     listSelect();
                     List<Corps> listegrad = corp.ToList();
                     PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                     return View("Index", model);

                    

                 }
                 else if (id_catg == 0 && strSearch != 0)
                 {
                     corp = corp.Where(m => m.Num_corp == strSearch);
                     listSelect();
                     List<Corps> listegrad = corp.ToList();
                     PagedList<Corps> model = new PagedList<Corps>(listegrad, page, pagesize);
                     return View("Index", model);
                    
                 }
                 listSelect();

                 List<Corps> listegrad2 = corp.ToList();
                 PagedList<Corps> modelg = new PagedList<Corps>(listegrad2, page, pagesize);
                 return View("Index", modelg);
                
             }
             listSelect();
             List<Corps> listeg = db.Corps.ToList();
             PagedList<Corps> modell = new PagedList<Corps>(listeg, page, pagesize);
             return View("index", modell);
        }

          
        // GET: /Corps/Details/5

        public ActionResult Details(int id = 0)
        {
            Corps corps = db.Corps.Find(id);
            if (corps == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(corps);
        }

        //
        // GET: /Corps/Create

        public ActionResult Create()
        {
            listSelect();
            return PartialView("Create");
        }

        //
        // POST: /Corps/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Corps corps)
        {
            if (ModelState.IsValid)
            {
                db.Corps.Add(corps);

                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    TempData["error"] = "existe";
                    return RedirectToAction("Index");
                }

                TempData["error"] = "add";
                return RedirectToAction("Index");
            }

            listSelect();
            TempData["error"] = "error";
            return RedirectToAction("Index");
        }

        //
        // GET: /Corps/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Corps corps = db.Corps.Find(id);
            if (corps == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            listSelect();
            return PartialView(corps);
        }

        //
        // POST: /Corps/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Corps corps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corps).State = EntityState.Modified;

                try {
                    db.SaveChanges();
                }
                catch
                {
                    TempData["error"] = "existe";
                    return RedirectToAction("Index");
                }
           
                TempData["error"] = "edit";
                return RedirectToAction("Index");
            }

            listSelect();
            return PartialView(corps);
        }

        //
        // GET: /Corps/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Corps corps = db.Corps.Find(id);
            if (corps == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(corps);
        }

        //
        // POST: /Corps/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corps corps = db.Corps.Find(id);

           

            try {
                db.Corps.Remove(corps);
                db.SaveChanges();
            }
            catch
            {
                TempData["error"] = "existe";
                return RedirectToAction("Index");
            }

          
            TempData["error"] = "delete";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}