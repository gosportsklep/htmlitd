namespace Sklep1.Migrations
{
    using Sklep1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sklep1.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sklep1.Models.AppDbContext context)
        {
            GetProducts().ForEach(p => context.ProductsList.Add(p));
        }
        private static List<Product> GetProducts()
        {
            var products = new List<Product>
       {
           new Product
           {
               Id = 0,
               Name = "R�kawice bramkarskie",
               Price = 29,
               Description = "R�kawice bramkarskie przeznaczone dla m�czyzn.",
               Category = "Pi�ka no�na"
           },
           new Product
           {
               Id = 1,
               Name = "Kask",
               Price = 299,
               Description = "Doskona�y kask narciarski wyposa�ony w regulowany system wentylacji",
               Category = "Narciarstwo",
           },
           new Product
           {
               Id = 2,
               Name = "Szorty",
               Price = 39,
               Description = "Dzieci�ce szorty k�pielowe to idealny wyb�r na letni wypoczynek nad morzem lub jeziorem czy do codziennego u�ytku w upalne dni.",
               Category = "Sporty wodne"
           }
    };

            return products;
        }
    }
}
