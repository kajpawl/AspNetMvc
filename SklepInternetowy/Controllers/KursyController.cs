using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepInternetowy.Controllers
{
    public class KursyController : Controller
    {
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lista kategorii
        public ActionResult Lista(string nazwaKategorii)
        {
            return View();
        }

        // GET: Lista kursów - bestsellery i nowości
        public ActionResult Szczegoly(string id)
        {
            return View();
        }
    }
}