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
    public class GradeController : Controller
    {
        private Base_Final_TGR2016Entities1 db = new Base_Final_TGR2016Entities1();

        //
        // GET: /Grade/


        public void listSelect()
        {
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp");


        }


        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            if (TempData["error"] == "add")
            {
                listSelect();
                List<Grade> listegrad = db.Grade.ToList();
                PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                ViewData["messag"] = "add";
                return View(model);

            }
            else if (TempData["error"] == "edit")
            {
                listSelect();
                List<Grade> listegrad = db.Grade.ToList();
                PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                ViewData["messag"] = "edit";
                return View(model);

            }
            else if (TempData["error"] == "delete")
            {
                listSelect();
                List<Grade> listegrad = db.Grade.ToList();
                PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                ViewData["messag"] = "delete";
                return View(model);

            }
            else if (TempData["error"] == "error")
            {
                listSelect();
                List<Grade> listegrad = db.Grade.ToList();
                PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                ViewData["messag"] = "error";
                return View(model);

            }
            else if (TempData["error"] == "existe")
            {
                listSelect();
                List<Grade> listegrad = db.Grade.ToList();
                PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                ViewData["messag"] = "existe";
                return View(model);

            }

            listSelect();
            List<Grade> listegradg = db.Grade.ToList();
            PagedList<Grade> modelg = new PagedList<Grade>(listegradg, page, pagesize);
            return View(modelg);



        }

        public ActionResult serach (int id_corp = 0, int strSearch = 0,int page = 1, int pagesize = 10)
        {

              if(id_corp !=0 || strSearch !=0)
              {
                  var grad = from b in db.Grade
                             select b;

                  if (id_corp == 0 && strSearch == 0)
                  {
                      listSelect();
                      List<Grade> listegrad = grad.ToList();
                     PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                     return View("Index", model);

                  }
                  else if (id_corp != 0 && strSearch == 0)
                  {
                      grad = grad.Where(m => m.id_corp == id_corp);
                      listSelect();
                      List<Grade> listegrad = grad.ToList();
                      PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                      return View("Index", model);
                     
                  }
                  else if (id_corp != 0 && strSearch != 0)
                  {
                      grad = grad.Where(m => m.id_corp == id_corp && m.Num_gard == strSearch);
                      listSelect();
                      List<Grade> listegrad = grad.ToList();
                      PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                      return View("Index", model);

                     

                  }
                  else if (id_corp == 0 && strSearch != 0)
                  {
                      grad = grad.Where(m => m.Num_gard == strSearch);
                      listSelect();
                      List<Grade> listegrad = grad.ToList();
                      PagedList<Grade> model = new PagedList<Grade>(listegrad, page, pagesize);
                      return View("Index", model); ;
                  }
                  listSelect();
                  listSelect();
                  List<Grade> listegrad2 = grad.ToList();
                  PagedList<Grade> modelg = new PagedList<Grade>(listegrad2, page, pagesize);
                  return View("Index", modelg);

              }


              listSelect();
              List<Grade> listeg = db.Grade.ToList();
              PagedList<Grade> modell = new PagedList<Grade>(listeg, page, pagesize);
              return View("index", modell);


           

        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(int id = 0)
        {
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(grade);
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            listSelect();
            return PartialView("Create");
        }

        //
        // POST: /Grade/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grade.Add(grade);
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
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            listSelect();
            return PartialView(grade);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;

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
            listSelect();
            return PartialView(grade);
        }

        //
        // GET: /Grade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return PartialView(grade);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grade.Find(id);
           
            try
            {
                db.Grade.Remove(grade);
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