using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Sklep1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("SklepDB")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}