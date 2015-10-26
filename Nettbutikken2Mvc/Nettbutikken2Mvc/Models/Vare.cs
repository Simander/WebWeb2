using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikken2Mvc.Models
{
    public class Vare
    {
        [Display(Name = "Varenummer")]
        public int Varenummer { get; set; }
        public string Kategori { get; set; }
        public string Navn { get; set; }
       
        public int kvantitet { get; set; }
        public string Pris { get; set; }
        public string Produsent { get; set; }

        internal static IQueryable<dbVare> Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public string beskrivelse { get; set; }

        public string VareBildeUrl { get; set; }
    }
    public class dbVare
    {
       [Key]
        public int VareID { get; set; }
        public int VareNr  { get; set; }
        public string KategoriNavn { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public decimal Pris { get; set; }

        public int AntallPaLager {get; set; }
       
        public string VareBildeUrl { get; set; }
    }

    

    public class VareDatabaseContext : DbContext
    {
        public DbSet<dbVare> vare { get; set; }
    }
}