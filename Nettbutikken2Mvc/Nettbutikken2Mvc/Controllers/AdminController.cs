using Nettbutikken2Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikken2Mvc.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult editVare()
        {
            return View();
        }
        public ActionResult addVareToDB()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addVareToDB(FormCollection innListe)
        {

            return View();
        }

        public ActionResult addKategoriToDB()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addKategoriToDB(FormCollection innListe)
        {
            bool sattInn = VareDbAksess.addKategori(innListe["Navn"]);
            if(sattInn == true)
            {
                RedirectToAction("listKategorier");
            }
            return View();
        }
        public ActionResult listKategorier()
        {
            var db = new Models.DatabaseContext();
            List<Models.dbVareKategori> listeAvKategorier = db.VareKategorier.ToList();
            return View(listeAvKategorier);
        }
    }
}