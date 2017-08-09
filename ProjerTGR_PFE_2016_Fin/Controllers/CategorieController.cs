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
    public class CategorieController : Controller
    {
        private Base_Final_TGR2016Entities1 db = new Base_Final_TGR2016Entities1();

        //
        // GET: /Categorie/

        public ActionResult Index(int page = 1, int pagesize = 5)

        {


            if (TempData["error"] == "add")
            {
                List<Categorie> listegra = db.Categorie.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegra, page, pagesize);
                ViewData["messag"] = "add";
                return View(model);
            }
            else if (TempData["error"] == "edit")
            {
                List<Categorie> listegra = db.Categorie.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegra, page, pagesize);
                ViewData["messag"] = "edit";
                return View(model);
            }
            else if (TempData["error"] == "delete")
            {
                List<Categorie> listegra = db.Categorie.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegra, page, pagesize);
                ViewData["messag"] = "delete";
                return View(model);

            }
            else if (TempData["error"]== "error")
            {
                List<Categorie> listegra = db.Categorie.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegra, page, pagesize);
                ViewData["messag"] = "error";
                return View(model);

            }
            else if (TempData["error"] == "existe")
            {
                List<Categorie> listegra = db.Categorie.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegra, page, pagesize);
                ViewData["messag"] = "existe";
                return View(model);
            }

            List<Categorie> listegrad = db.Categorie.ToList();
            PagedList<Categorie> modell = new PagedList<Categorie>(listegrad, page, pagesize);
            return View(modell);
               
              
        }
              

               
                   
         
               
   
    
      


        public ActionResult serach (string strsearch,int page = 1, int pagesize = 5)
        {
           
  
            if (strsearch !="")
            {
                var id = Convert.ToInt32(strsearch);
                string text = strsearch;
                var book = from b in db.Categorie
                           select b;
                var liste = from c in book

                            select c.Nom_catg;

                ViewBag.strsearch = new SelectList(liste.Distinct());

                if (id > 0 || text != "")
                    book = book.Where(m => m.Num_catg == id || m.Nom_catg == text);
                List<Categorie> listegrad = book.ToList();
                PagedList<Categorie> model = new PagedList<Categorie>(listegrad, page, pagesize);
                return View("Index", model);
            }
            List<Categorie> listeg = db.Categorie.ToList();
            PagedList<Categorie> modell = new PagedList<Categorie>(listeg, page, pagesize);
            return View("index",modell);   
           
        }
        

        //
        // GET: /Categorie/Details/5

        public ActionResult Details(int id = 0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(categorie);
        }

        //
        // GET: /Categorie/Create

        public ActionResult Create()
        {
            return PartialView("Create");
        }

        //
        // POST: /Categorie/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categorie.Add(categorie);

                try
                {
                     db.SaveChanges();
                }catch
                {
                     TempData["error"] = "existe";
                return RedirectToAction("Index");

                }
               
                TempData["error"] = "add";
                return RedirectToAction("Index");
            }
            TempData["error"] = "error";
            return RedirectToAction("Index");
        }

        //
        // GET: /Categorie/Edit/5

        public ActionResult Edit(int id =0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(categorie);
        }

     
        

        //
        // POST: /Categorie/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                try
                {
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
            return PartialView(categorie); 
        }

        //
        // GET: /Categorie/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(categorie);
        }

        //
        // POST: /Categorie/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);

            try {
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