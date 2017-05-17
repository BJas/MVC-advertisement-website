using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MvcKrisha.Models;
using System.Linq;
using System.Web;

namespace MvcKrisha.DAL
{
    public class KrishaContext : DbContext
    {
        public KrishaContext() : base("KrishaContext")
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Flat> Flats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}