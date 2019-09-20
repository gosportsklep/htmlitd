﻿using Sklep1.Models;
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
            foreach (var order in db.Orders)
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
                order.ProductsIds += productId + ",";

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
                    order.ProductsIds += productId + ",";

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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                var users = db.Users;
                users.Add(user);
                db.SaveChanges();
                ViewBag.Message = "Successfully registered";
                return View("Login");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " +
                                       validationError.ErrorMessage);
                    }
                }

                ViewBag.Message = "Something wrong";

                return View("Register");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            var loginsDb = db.Logins;
            var usersDb = db.Users;
            var user = usersDb.First(x => x.Username == loginForm.Username && x.Password == loginForm.Password);
            if (user == null)
            {
                //login fail
                return View("Login");

            }
            else
            {
                var token = (user.Password.GetHashCode() + user.Username.GetHashCode()).ToString();
                loginsDb.Add(new LoginDatabaseRow(user.Id, token));
                db.SaveChanges();
                return View("Index");
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

        public ActionResult Danedowysylki()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Danedowysylki(UserData userData)
        {
            db.UserOrderData.Add(userData);
            db.SaveChanges();
            return RedirectToAction("DanePrzelew");
        }

        public ActionResult DanePrzelew()
        {
            
            return View();
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
                        Subject = senderEmail + " " + subject,
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