using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjerTGR_PFE_2016_Fin.Models;
using System.IO;
using System.Data.SqlClient;

namespace ProjerTGR_PFE_2016_Fin.Controllers
{
    public class ParametrageController : Controller
    {

        // GET: /Parametrage_BO/

        private Base_Final_TGR2016Entities1 db = new Base_Final_TGR2016Entities1();

        public void listSelect ()
        {
            ViewBag.id_catg = new SelectList(db.Categorie,"id_catg", "Nom_catg");
            ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp");
            ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad");

        }

        public ActionResult Index()
        {


            listSelect();
            return View();
        }



        public ActionResult Create(HttpPostedFileBase Bulletins, HttpPostedFileBase Note, int numBO, int id_catg=0, int id_grad=0, int id_corp=0)
        {
            var fileNameBulletins = Path.GetFileName(Bulletins.FileName);
            var fileNameBNote = Path.GetFileName(Note.FileName);
            var id_notepar="";
            var id_buo = "";
          
            if ((!System.IO.File.Exists(@"D:\TGR\BulletinsoOfficiel\" + fileNameBulletins)) && (!System.IO.File.Exists(@"D:\TGR\NoteParametrage\" + fileNameBNote)))
            {
               if (Bulletins.ContentLength > 0)
                {

                    /* ADD  Bulletins */

                    var PatchBulletins = @"D:\TGR\BulletinsoOfficiel\" + fileNameBulletins;
                    var nameBulletins = Bulletins.FileName;
                    var tailleBulletins = (Bulletins.ContentLength);
                    var typeBulletins = Path.GetExtension(Bulletins.FileName);
                    Bulletins.SaveAs(@"D:\TGR\BulletinsoOfficiel\" + fileNameBulletins);
                    try
                    {
                      
                        Connexion.cmd = new SqlCommand("insert into Bulletins_officiel(name_bo,type_bo,patch_bo,taille_bo) values ('" + nameBulletins + "','" + typeBulletins + "' ,'" + PatchBulletins + "' ," + tailleBulletins + ")", Connexion.cn);
                        Connexion.connexion();
                        Connexion.cmd.ExecuteNonQuery();
                        Connexion.deconnexion();
                    }
                    catch
                    {

                        ViewBag.id_catg = new SelectList(db.Categorie, "id_catg", "Nom_catg");
                        ViewBag.id_corp = new SelectList(db.Corps, "id_corp", "Nom_corp");
                        ViewBag.id_grad = new SelectList(db.Grade, "id_grad", "Nom_grad");
                        ViewData["result"] = "probleme";
                        return View("Index");
                    }

                    var PatchNote = @"D:\TGR\NoteParametrage\" + fileNameBNote;
                    var nameNote = Note.FileName;
                    var tailleNote = (Note.ContentLength / 1024);
                    var typeNote = Path.GetExtension(Note.FileName);
                    Note.SaveAs(@"D:\TGR\NoteParametrage\" + fileNameBNote);


                    try {
                        /* ADD  Bulletins */
                    
                        Connexion.cmd = new SqlCommand("insert into Note_Parametrage(name_par,type_par,patch_par,taille_par) values ('" + nameNote + "','" + typeNote + "' ,'" + PatchNote + "' ," + tailleNote + ")", Connexion.cn);
                        Connexion.connexion();
                        Connexion.cmd.ExecuteNonQuery();
                        Connexion.deconnexion();

                    
                    }
                    catch
                    {
                        listSelect();
                        ViewData["result"] = "probleme";
                        return View("Index");
                    }
                   


                   
                   /* requiper id_note*/
                    Connexion.cmd = new SqlCommand("select id_par  from Note_Parametrage where name_par ='" + nameNote + "' ", Connexion.cn);
                    Connexion.connexion();
                    Connexion.rd = Connexion.cmd.ExecuteReader();
                    Connexion.rd.Read();
                    id_notepar = Connexion.rd["id_par"].ToString();
                    Connexion.deconnexion();

                    /* requiper id_BO*/
                    Connexion.cmd = new SqlCommand("select id_bo  from Bulletins_officiel where name_bo ='" + nameBulletins + "' ", Connexion.cn);
                    Connexion.connexion();
                    Connexion.rd = Connexion.cmd.ExecuteReader();
                    Connexion.rd.Read();
                    id_buo = Connexion.rd["id_bo"].ToString();
                    Connexion.deconnexion();



                   /* ADD NOTE AND PARAMETRAGE */

                    try
                    {
                        Connexion.cmd = new SqlCommand("insert into Union_ref_bo(Num_bo,id_bo,id_par,id_catg,id_corp,id_grad) values (" + numBO + " ," + id_buo + " , " + id_notepar + ", " + id_catg + ", " + id_corp + ", " + id_grad + ")", Connexion.cn);
                        Connexion.connexion();
                        Connexion.cmd.ExecuteNonQuery();
                        Connexion.deconnexion();
                    }
                    catch
                    {
                        listSelect();
                        ViewData["result"] = "probleme";
                        return View("Index");

                    }

                  


                    
                }


               listSelect();
             ViewData["result"]="bien";
               return View("Index");

               
            }


            listSelect();
            ViewData["result"] = "error";
            return View("Index");

           
        }
        
       

    }
}
