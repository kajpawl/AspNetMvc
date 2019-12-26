﻿using SklepInternetowy.DAL;
using SklepInternetowy.Models;
using SklepInternetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepInternetowy.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();

        // GET: Home
        public ActionResult Index()
        {
            var kategorie = db.Kategorie.ToList();

            var nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();

            var bestsellery = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();

            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Bestsellery = bestsellery
            };

            return View(vm);
        }

        // GET: Static pages
        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }
    }
}