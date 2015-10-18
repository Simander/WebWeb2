using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikken2Mvc.Models
{
    public class VareDbAksess
    {
        public static bool addKategori(string kategori)
        {
            try
            {
                using(var db = new Models.DatabaseContext())
                {
                    var existKategori = db.VareKategorier
                        .FirstOrDefault(x => x.Navn.Equals(kategori));
                    if (existKategori != null)
                    {
                        dbVareKategori vk = new dbVareKategori();
                        vk.Navn = kategori;
                        db.VareKategorier.Add(vk);
                        db.SaveChanges();
                        return true;
                    }
                    else return false;
                }
            }
            catch(Exception Feil)
            {
                return false;
            }
        }
        public bool addVare(dbVare v, dbVareKategori vk)
        {
            try
            {
                using (var db = new Models.DatabaseContext())
                {
                    
                    var existKategori = db.VareKategorier
                        .FirstOrDefault(x => x.Navn.Equals(vk.Navn));
                    if(existKategori == null)
                    {
                        db.VareKategorier.Add(v.Kategori);
                    }
                    else
                    {
                        v.Kategori = existKategori;
                    }
                    var existVare = db.Varer
                            .FirstOrDefault(k => k.Navn == v.Navn);
                    if (existVare == null)
                    {
                        db.Varer.Add(v);

                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception) {
                return false;
            }
        }

        public bool slettVare(int vareID)
        {
            return false;
        }
        public bool oppdaterVAre(dbVare vare)
        {
            return false;
        }
    }
}