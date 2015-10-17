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
    }
}