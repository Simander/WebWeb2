using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikken2Mvc.Models;

namespace Nettbutikken2Mvc.Controllers
{
    public class BrukerController : Controller
    {
        // GET: Bruker
        public ActionResult Index()
        {
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }

            return View();
        }


        // GET: /Bruker/LoggUt
        public ActionResult LoggUt()
        {
            Session["LoggetInn"] = false;
            Session["Bruker"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: /Bruker/LoggInn
        public ActionResult LoggInn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoggInn(FormCollection innListe)
        {
            var brukernavn = innListe["Epost"];
            var passord = Logikk.hashPword(innListe["Passord"]);
            try
            {
                using (var db = new DatabaseContext())
                {

                    var funnetBruker = db.Kunder
                       .FirstOrDefault(k => k.Epost == brukernavn);
                    if (funnetBruker == null) //fant ikke poststed, må legge inn et nytt
                    {
                        return View();
                    }
                    else
                    {
                        if (funnetBruker.Passord.SequenceEqual(passord))
                        {
                            Session["LoggetInn"] = true;
                            Session["Bruker"] = funnetBruker;
                            return RedirectToAction("Index", "Home");
                            // return "Kundenr: " + ((Kunde)Session["Bruker"]).KundeNR + " | Brukernavn: " + ((Kunde)Session["Bruker"]).Epost + " er logget inn!";
                        }
                        //return "funnetBruker.Passord: " + funnetBruker.Passord + " | innskrevet hash: " + passord;
                    }
                }
                return View();
            }
            catch (Exception feil)
            {

                return View(feil);
            }
        }
        //GET: /Bruker/CreateNyKunde
        public ActionResult NyKunde()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NyKunde(FormCollection innListe)
        {
            if (ModelState.IsValid)
            {
                /*
                return "Fornavn: " + innListe["Fornavn"] + "\nEtternavn: " + innListe["Etternavn"] + "\nAdresse: " + innListe["Adresse"] +
                     "\nTelefonnr: " + innListe["Telefonnummer"] + "\nEpost: " + innListe["Epost"] + "\nPassord: " + innListe["Passord"] +
                     "\nPoststed: " + innListe["Poststed"] + "\nPostnr: " + innListe["Postnummer"];
                */
               // Console.WriteLine("In the mucK");
                try
                {
                    using (var db = new Models.DatabaseContext())
                    {
                        Session["Bruker"] = null;
                        Session["LoggetInn"] = false;
                        var nyKunde = new Models.dbKunde();
                        nyKunde.Fornavn = innListe["Fornavn"];
                        nyKunde.Etternavn = innListe["Etternavn"];
                        nyKunde.Adresse = innListe["Adresse"];
                        nyKunde.Telefonnummer = innListe["Telefonnummer"];
                        nyKunde.Epost = innListe["Epost"];
                        if (innListe["Passord"].Equals(innListe["PassordKopi"]))
                        {
                            nyKunde.Passord = (Logikk.hashPword(innListe["Passord"]));
                        }
                       
                          //Kan ikke bruke dette array i LINQ nedenfor
                          string innPostNr = innListe["Postnummer"];
                          var funnetPoststed = db.Poststeder
                              .FirstOrDefault(p => p.Postnummer == innPostNr);
                          if (funnetPoststed == null) //fant ikke poststed, må legge inn et nytt
                          {
                              var nyttPoststed = new Models.dbPoststed();
                              nyttPoststed.Postnummer = innListe["Postnummer"];
                              nyttPoststed.Poststed = innListe["Poststed"];
                              db.Poststeder.Add(nyttPoststed);
                              //det nye poststedet legges i den nye brukeren
                              nyKunde.Poststed = nyttPoststed;
                          }
                          else
                          {
                              //fant poststedet, legger det inn på bruker
                              nyKunde.Poststed = funnetPoststed;
                          }
                        var exist = db.Kunder
                            .FirstOrDefault(k => k.Epost == nyKunde.Epost);
                        if (exist == null)
                        {
                            db.Kunder.Add(nyKunde);
                       
                            Session["Bruker"] = nyKunde;
                          
                        }
                        db.SaveChanges();
                        return RedirectToAction("RegistreringsResultat");


                    }
                }
                catch (Exception feil)
                {
                return View();
                }
            }
            else return View();
        }
        public ActionResult RegistreringsResultat()
        {
            var db = new Models.DatabaseContext();
            List<Models.dbKunde> listeAvKunder = db.Kunder.ToList();
            return View(listeAvKunder);
        }

        
    }
}