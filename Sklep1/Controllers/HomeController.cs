using Sklep1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep1.Controllers
{
    public class HomeController : Controller
    {

        private AppDbContext db = new AppDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sklep()
        {
            return View();
        }

        public ActionResult Podsumowanie()
        {
            return View();
        }

        public ActionResult Login()
        {

            //Example of creating user check database model for more information
            User u = new User();
            u.Username = "user";
            u.Password = "password";
            u.Email = "test@test.com";

            //TOTEN

            db.Users.Add(u);
            try
            {
                db.SaveChanges();
                ViewBag.Message = "User has been added";
                return View();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                ViewBag.Message = "Something wrong";

                return View();
            }
        }

        public ActionResult Koszyk()
        {
            //Get products list 
            var productList = db.Products;
            foreach (var product in productList)
            {
                System.Diagnostics.Debug.WriteLine(product.Name);
            }

            //Check if any product has category "meble"
            var isThatCategoryInDB = db.Products.Any(r => r.Category == "pokoj");

            System.Diagnostics.Debug.WriteLine(isThatCategoryInDB);


            //GetProductByCategory 
            var getProductsByCategory = db.Products.Where(r => r.Category == "pokoj");

            foreach (var product in getProductsByCategory)
            {
                System.Diagnostics.Debug.WriteLine("getProductByCategory productName " + product.Name + " categoryName " + product.Category);
            }
            return View();
        }

        public ActionResult Kontakt()
        {
            //Example of creating product check database model for more information
            Product p = new Product();
            p.Name = "stol";
            p.Description = "Opis stolu 10 znakow";
            p.Category = "pokoj";

            Product p1 = new Product();
            p1.Name = "fotel";
            p1.Description = "Opis fotela 10 znakow";
            p1.Category = "pokoj";

            Product p2 = new Product();
            p2.Name = "kuchenka";
            p2.Description = "Opis kuchenki 10 znakow";
            p2.Category = "kuchnia";

            db.Products.Add(p);
            db.Products.Add(p1);
            db.Products.Add(p2);
            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                ViewBag.Message = "Something wrong";
            }
            return View();
        }

    }
}