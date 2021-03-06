﻿using NLog;
using SklepInternetowy.DAL;
using SklepInternetowy.Infrastructure;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private KursyContext db = new KursyContext();
        ICacheProvider cache = new DefaultCacheProvider();

        // GET: Home
        public ActionResult Index()
        {
            // logger.Info("Jesteś na stronie głównej.");

            List<Kategoria> kategorie;
            if (cache.IsSet(Consts.KategorieCacheKey))
            {
                kategorie = cache.Get(Consts.KategorieCacheKey) as List<Kategoria>;
            }
            else
            {
                kategorie = db.Kategorie.ToList();
                cache.Set(Consts.KategorieCacheKey, kategorie, 60);
            }

            List<Kurs> nowosci;
            if (cache.IsSet(Consts.NowosciCacheKey))
            {
                nowosci = cache.Get(Consts.NowosciCacheKey) as List<Kurs>;
            }
            else
            {
                nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
                cache.Set(Consts.NowosciCacheKey, nowosci, 60);
            }

            List<Kurs> bestsellery;
            if (cache.IsSet(Consts.BestselleryCacheKey))
            {
                bestsellery = cache.Get(Consts.BestselleryCacheKey) as List<Kurs>;
            }
            else
            {
                bestsellery = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestselleryCacheKey, bestsellery, 60);
            }

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