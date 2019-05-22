using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sklep()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Koszyk()
        {
            return View();
        }

        public ActionResult Kontakt()
        {
            return View();

        }
       
    }
}