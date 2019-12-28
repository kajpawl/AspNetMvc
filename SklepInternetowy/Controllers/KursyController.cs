using SklepInternetowy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepInternetowy.Controllers
{
    public class KursyController : Controller
    {
        private KursyContext db = new KursyContext();
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lista kursów - bestsellery i nowości
        public ActionResult Lista(string nazwaKategorii)
        {
            var kategoria = db.Kategorie.Include("Kursy").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategorii.ToUpper()).Single();
            var kursy = kategoria.Kursy.ToList();
            return View(kursy);
        }

        // GET: pojedynczy kurs
        public ActionResult Szczegoly(int id)
        {
            var kurs = db.Kursy.Find(id);
            return View(kurs);
        }

        // GET: lista kategorii - partial view
        [ChildActionOnly]
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu", kategorie);
        }
    }
}