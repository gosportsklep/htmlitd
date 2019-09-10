using Sklep1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
           foreach(var order in db.Orders)
            {
              System.Diagnostics.Debug.WriteLine("Order products " + order.ProductsIds);
            }

              return View(db.ProductsList.ToList());
        }

        [HttpPost]
        public ActionResult AddProductToOrder(int productId)
        {
            var orders = db.Orders;
            if (orders?.Any() != true || !orders.Any(r => r.OrderStatus == "Pending"))
            {
                Order order = new Order();
                order.ProductsIds +=  productId + ",";

                order.OrderStatus = "Pending";
                order.CreatedOn = DateTime.Now;

                db.Orders.Add(order);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Message " + "Order has been created");
            }
            else
            {
                var currentOrder = db.Orders.FirstOrDefault(x => x.OrderStatus == "Pending");

                var order = db.Orders.FirstOrDefault(x => x.Id == currentOrder.Id);

                List<string> productsIds = order.ProductsIds.Split(',').ToList();

                if (!productsIds.Any(id => id == productId.ToString()))
                {
                    order.ProductsIds +=  productId + ",";

                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Message " + "Products has been added to order");
                  }
                  else
                  {
                    System.Diagnostics.Debug.WriteLine("Message " + "This product is already in this order");
                  }
            }
            return RedirectToAction("Sklep");
        }

        public ActionResult Login()
        {

            //Example of creating user check database model for more information
            User u = new User();
            u.Username = "user";
            u.Password = "password";
            u.Email = "test@test.com";

            //TOTEN

            db.UsersList.Add(u);
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

        public ActionResult FinishOrder(int orderId)
        {
            var order = db.Orders.SingleOrDefault(o => o.Id == orderId);

            order.OrderStatus = "Complete";
            db.SaveChanges();

            Order newOrder = new Order();
            newOrder.ProductsIds = "";

            newOrder.OrderStatus = "Pending";
            newOrder.CreatedOn = DateTime.Now;

            db.Orders.Add(newOrder);
            db.SaveChanges();

            return RedirectToAction("Sklep");
        }

        [HttpPost]
        public ActionResult DeleteProductFromOrder(int productId, int orderId)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == orderId);

            List<string> productsIds = order.ProductsIds.Split(',').ToList();
            productsIds.Remove(productId.ToString());

            order.ProductsIds = String.Join(",", productsIds);
            db.SaveChanges();
            return RedirectToAction("Koszyk");
        }

            public ActionResult Koszyk()
        {

            //   GetProducts().ForEach(p => db.ProductsList.Add(p));
            //db.SaveChanges();

            var order = db.Orders.SingleOrDefault(o => o.OrderStatus == "Pending");
            List<Product> products = db.ProductsList.ToList();


            if (order == null)
            {
                Order newOrder = new Order();
                order.ProductsIds = "";

                order.OrderStatus = "Pending";
                order.CreatedOn = DateTime.Now;

                order = newOrder;
            }

                Tuple<Order, List<Product>> tuple = new Tuple<Order, List<Product>>(order, products);
                return View(tuple);
        }

        private object Tuple(List<Order> orders, List<Product> products)
        {
            throw new NotImplementedException();
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

        public ActionResult Kontakt()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Kontakt(string sender, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(sender, "Sender");
                    var receiverEmail = new MailAddress("gosportsklp@gmail.com", "Receiver");
                    var password = "gosport123#$";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(receiverEmail.Address, password)

                    };

                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = senderEmail +" "+ subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
                return View();
            }

            catch (Exception e)
            {
                ViewBag.Error = e.GetBaseException().ToString();
               // ViewBag.Error = "Nie udało się wysłać, spróbuj ponownie lub póżniej";
            }
            return View();
        }
       
    }
}