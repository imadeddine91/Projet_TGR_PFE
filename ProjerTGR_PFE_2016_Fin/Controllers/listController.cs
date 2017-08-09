using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjerTGR_PFE_2016_Fin.Models;
using System.IO;
using System.Data.SqlClient;

namespace ProjerTGR_PFE_2016_Fin.Controllers
{
    public class listController : Controller
    {
        private Base_Final_TGR2016Entities1 db = new Base_Final_TGR2016Entities1();

        //
        // GET: /Default1/

        public void listSelect()
        {
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg");
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp");
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad");

        }
        public ActionResult Index(int id_catg = 0, int id_corp = 0, int id_grad = 0)
        {

            var book = from b in db.Union_ref_bo
                       select b;

            if (id_catg == 0 && id_corp == 0 && id_grad == 0)               /* 0 0 0 */
            {
                listSelect();
                return View(book);
            }
            else if (id_catg != 0 && id_corp == 0 && id_grad == 0)            /* 1 0 0 */
            {
                book = book.Where(m => m.id_catg == id_catg);
                listSelect();

                return View(book);

            }
            else if (id_catg == 0 && id_corp != 0 && id_grad == 0)            /* 0 1 0 */
            {
                book = book.Where(m => m.id_corp == id_corp);
                listSelect();

                return View(book);

            }
            else if (id_catg == 0 && id_corp == 0 && id_grad != 0)            /* 0 0 1 */
            {
                book = book.Where(m => m.id_grad == id_grad);
                listSelect();

                return View(book);

            }
            else if (id_catg != 0 && id_corp != 0 && id_grad == 0)            /* 1 1 0 */
            {
                book = book.Where(m => m.id_catg == id_catg && m.id_corp == id_corp);
                listSelect();

                return View(book);

            }
            else if (id_catg != 0 && id_corp == 0 && id_grad != 0)             /* 1 0 1 */
            {
                book = book.Where(m => m.id_catg == id_catg && m.id_grad == id_grad);
                listSelect();

                return View(book);
            }
            else if (id_catg == 0 && id_corp != 0 && id_grad != 0)             /* 0 1 1 */
            {
                book = book.Where(m => m.id_corp == id_corp && m.id_grad == id_grad);
                listSelect();

                return View(book);
            }
            else if (id_catg != 0 && id_corp != 0 && id_grad != 0)
            {
                book = book.Where(m => m.id_corp == id_corp && m.id_grad == id_grad && m.id_corp == id_corp);
                listSelect();

                return View(book);
            }

            listSelect();
            var union_ref_bo = db.Union_ref_bo.Include(u => u.Bulletins_officiel).Include(u => u.Categorie).Include(u => u.Corps).Include(u => u.Grade).Include(u => u.Note_Parametrage);
            return View(union_ref_bo.ToList());
        }

        //
        
        public ActionResult recharch(int id_catg=0, int id_corp=0, int id_grad=0)
        {

            /* 0 0 0 */

            var book = from b in db.Union_ref_bo
                       select b;

            if (id_catg == 0 && id_corp == 0 && id_grad == 0)               /* 0 0 0 */
            {
                listSelect();
                return View("index");
            }
            else if (id_catg != 0 && id_corp == 0 && id_grad == 0)            /* 1 0 0 */
            {
            

                return View();

            }
            else if (id_catg == 0 && id_corp != 0 && id_grad == 0)            /* 0 1 0 */
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();

            }
            else if (id_catg == 0 && id_corp == 0 && id_grad != 0)            /* 0 0 1 */
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();
            }
            else if (id_catg != 0 && id_corp != 0 && id_grad == 0)            /* 1 1 0 */
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();
            }
            else if (id_catg != 0 && id_corp == 0 && id_grad != 0)             /* 1 0 1 */
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();
            }
            else if (id_catg == 0 && id_corp != 0 && id_grad != 0)             /* 0 1 1 */
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();
            }
            else if (id_catg != 0 && id_corp != 0 && id_grad != 0)
            {
                ViewData["id_catg"] = id_catg;
                ViewData["id_corp"] = id_corp;
                ViewData["id_grad"] = id_grad;
                return View();
            }
            return View();

           
        }
        public ActionResult show(int id = 0)
        {
            FileStream fs;
            Connexion.cmd = new SqlCommand("select patch_par,type_par from Note_Parametrage where id_par =" + id + "", Connexion.cn);
            Connexion.cn.Open();
            Connexion.rd = Connexion.cmd.ExecuteReader();
            Connexion.rd.Read();
            var pat = Connexion.rd["patch_par"].ToString();
            var type = Connexion.rd["type_par"].ToString();

            Connexion.cn.Close();

            if (type == ".pdf")
            {
                fs = new FileStream(@"" + pat, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            else
            {

                var filePath = @"" + pat;
                return File(filePath, "application/pdf", filePath);



            }

        }

        public ActionResult Open(int id =0)
        {
            FileStream fs;

            Connexion.cmd = new SqlCommand("select patch_bo ,type_bo from Bulletins_officiel where id_bo =" + id + "", Connexion.cn);
            Connexion.cn.Open();
            Connexion.rd = Connexion.cmd.ExecuteReader();
            Connexion.rd.Read();
            var pat = Connexion.rd["patch_bo"].ToString();
            var type = Connexion.rd["type_bo"].ToString();

            Connexion.cn.Close();

            if(type==".pdf")
            {
              fs = new FileStream(@"" + pat, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            else
            {

                var filePath = @"" + pat;
                return File(filePath, "application/pdf", filePath);

               
                
            }
            
            

        }

        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Union_ref_bo union_ref_bo = db.Union_ref_bo.Find(id);
            if (union_ref_bo == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(union_ref_bo);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            ViewBag.id_bo = new SelectList(db.Bulletins_officiel, "id_bo", "name_bo");
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg");
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp");
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad");
            ViewBag.id_par = new SelectList(db.Note_Parametrage, "id_par", "name_par");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Union_ref_bo union_ref_bo)
        {
            if (ModelState.IsValid)
            {
                db.Union_ref_bo.Add(union_ref_bo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bo = new SelectList(db.Bulletins_officiel, "id_bo", "name_bo", union_ref_bo.id_bo);
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg", union_ref_bo.id_catg);
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp", union_ref_bo.id_corp);
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad", union_ref_bo.id_grad);
            ViewBag.id_par = new SelectList(db.Note_Parametrage, "id_par", "name_par", union_ref_bo.id_par);
            return View(union_ref_bo);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Union_ref_bo union_ref_bo = db.Union_ref_bo.Find(id);
            if (union_ref_bo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bo = new SelectList(db.Bulletins_officiel, "id_bo", "name_bo", union_ref_bo.id_bo);
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg", union_ref_bo.id_catg);
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp", union_ref_bo.id_corp);
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad", union_ref_bo.id_grad);
            ViewBag.id_par = new SelectList(db.Note_Parametrage, "id_par", "name_par", union_ref_bo.id_par);
            return View(union_ref_bo);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Union_ref_bo union_ref_bo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(union_ref_bo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bo = new SelectList(db.Bulletins_officiel, "id_bo", "name_bo", union_ref_bo.id_bo);
            ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg", union_ref_bo.id_catg);
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp", union_ref_bo.id_corp);
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad", union_ref_bo.id_grad);
            ViewBag.id_par = new SelectList(db.Note_Parametrage, "id_par", "name_par", union_ref_bo.id_par);
            return View(union_ref_bo);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Union_ref_bo union_ref_bo = db.Union_ref_bo.Find(id);
            if (union_ref_bo == null)
            {
                TempData["error"] = "error";
                return RedirectToAction("Index");
            }
            return PartialView(union_ref_bo);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var id_para = "";
            var id_bo = "";

            Union_ref_bo union_ref_bo = db.Union_ref_bo.Find(id);
            db.Union_ref_bo.Remove(union_ref_bo);
            db.SaveChanges();

            Connexion.cmd = new SqlCommand("select id_par ,id_bo from Union_ref_bo where id_un=" + id + "", Connexion.cn);
            Connexion.connexion();
            Connexion.rd = Connexion.cmd.ExecuteReader();
            Connexion.rd.Read();
            id_para = Connexion.rd["id_par"].ToString();
            id_bo = Connexion.rd["id_bo"].ToString();
            Connexion.deconnexion();

            var bo = Convert.ToInt16(id_bo);
            int note = Convert.ToInt16(id_para);
            

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}