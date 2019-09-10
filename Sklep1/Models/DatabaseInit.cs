using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sklep1.Models
{
    public class DatabaseInit : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
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
               Name = "Rękawice bramkarskie",
               Price = 29,
               Description = "Rękawice bramkarskie przeznaczone dla mężczyzn.",
               Category = "Piłka nożna"
           },
           new Product
           {
               Id = 1,
               Name = "Kask",
               Price = 299,
               Description = "Doskonały kask narciarski wyposażony w regulowany system wentylacji",
               Category = "Narciarstwo",
           },
           new Product
           {
               Id = 2,
               Name = "Szorty",
               Price = 39,
               Description = "Dziecięce szorty kąpielowe to idealny wybór na letni wypoczynek nad morzem lub jeziorem czy do codziennego użytku w upalne dni.",
               Category = "Sporty wodne"
           }
    };
   
        return products;
      }
    }
}