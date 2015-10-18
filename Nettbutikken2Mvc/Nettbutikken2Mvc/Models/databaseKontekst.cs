using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Nettbutikken2Mvc.Models
{
   
        public class DatabaseContext : DbContext
        {
            public DatabaseContext()
                : base("name=butikkDatabase")
            {
                Database.CreateIfNotExists();
            }

            public DbSet<dbKunde> Kunder { get; set; }
            public DbSet<dbPoststed> Poststeder { get; set; }


            public DbSet<dbVare> Varer { get; set; }
            public DbSet<dbVareKategori> VareKategorier {get;set;}

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }

        }
    
}