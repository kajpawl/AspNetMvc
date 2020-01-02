using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SklepInternetowy.App_Start;
using SklepInternetowy.DAL;
using SklepInternetowy.Infrastructure;
using SklepInternetowy.Models;
using SklepInternetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SklepInternetowy.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykManager koszykManager;
        private ISessionManager sessionManager { get; set; }
        private KursyContext db;

        public KoszykController()
        {
            db = new KursyContext();
            sessionManager = new SessionManager();
            koszykManager = new KoszykManager(sessionManager, db);
        }

        // widok koszyka
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykManager.PobierzKoszyk();
            var cenaCalkowita = koszykManager.PobierzWartoscKoszyka();
            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita = cenaCalkowita
            };
            return View(koszykVM);
        }

        public ActionResult DodajDoKoszyka(int id)
        {
            koszykManager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }

        public int PobierzIloscElementowKoszyka()
        {
            return koszykManager.PobierzIloscPozycjiKoszyka();
        }

        public ActionResult UsunZKoszyka(int kursId)
        {
            int iloscPozycji = koszykManager.UsunZKoszyka(kursId);
            int iloscPozycjiKoszyka = koszykManager.PobierzIloscPozycjiKoszyka();
            decimal wartoscKoszyka = koszykManager.PobierzWartoscKoszyka();

            var wynik = new KoszykUsuwanieViewModel
            {
                IdPozycjiUsuwanej = kursId,
                IloscPozycjiUsuwanej = iloscPozycji,
                KoszykCenaCalkowita = wartoscKoszyka,
                KoszykIloscPozycji = iloscPozycjiKoszyka
            };
            return Json(wynik);
        }

        public async Task<ActionResult> Zaplac()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var zamowienie = new Zamowienie
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy = user.DaneUzytkownika.KodPocztowy,
                    Email = user.DaneUzytkownika.Email,
                    Telefon = user.DaneUzytkownika.Telefon
                };
                return View(zamowienie);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Zaplac", "Koszyk") });
        }

        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienie zamowienieSzczegoly)
        {
            if (ModelState.IsValid)
            {
                // pobieramy id aktualnie zalogowanego użytkownika
                var userId = User.Identity.GetUserId();

                // utworzenie obiektu zamowienia na poddstawie tego co mamy w koszyku
                var newOrder = koszykManager.UtworzZamowienie(zamowienieSzczegoly, userId);

                // szczegóły użytkownika - aktualizacja danych
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);

                // opróżniamy nasz koszyk zakupów
                koszykManager.PustyKoszyk();

                // Hangfire: zadanie wywoływane w tle: natychmiast / z opóźnieniem / cyklicznie
                string url = Url.Action("PotwierdzenieZamowieniaEmail", "Koszyk", new { zamowienieId = newOrder.ZamowienieID, nazwisko = newOrder.Nazwisko }, Request.Url.Scheme);
                BackgroundJob.Enqueue(() => Call(url));

                return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
                return View(zamowienieSzczegoly);
        }

        public void Call(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.GetResponseAsync();
        }

        public ActionResult PotwierdzenieZamowieniaEmail(int zamowienieId, string nazwisko)
        {
            // pobieramy informacje o realizowanym zamówieniu
            var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Kurs")
                             .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);

            if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();
            email.To = zamowienie.Email;
            email.From = "kjtk@o2.pl";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult PotwierdzenieZamowienia()
        {
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}