using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikken2Mvc.Models
{
    public class VareDbAksess
    {
        public bool addVare(dbVare v, dbVareKategori vk)
        {
            try
            {
                using (var db = new Models.DatabaseContext())
                {
                    
                    var existKategori = db.VareKategori
                        .FirstOrDefault(x => x.Navn.Equals(vk.Navn));
                    if(existKategori == null)
                    {
                        db.VareKategori.Add(v.Kategori);
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