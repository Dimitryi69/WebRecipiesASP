using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CourseWeb.Models.DBModels
{
    public class RECIPIESCONTEXT : DbContext
    {
        //static RECIPIESCONTEXT()
        //{
        //    Database.SetInitializer<RECIPIESCONTEXT>(null);
        //}
        //public RECIPIESCONTEXT() : base()
        //{
        //}
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentsLink> ComponentsLinks { get; set; }
        public DbSet<LeftComponentsLink> LeftComponentsLink { get; set; }
        public DbSet<Recipy> Recipies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Recipy>()
            .HasMany(c => c.ComponentsLink)
        .WithRequired(o => o.Recipy)
        .WillCascadeOnDelete(true);
            modelBuilder.Entity<Recipy>()
            .HasMany(c => c.LeftComponentsLink)
        .WithRequired(o => o.Recipy)
        .WillCascadeOnDelete(true);
        }
    }
}