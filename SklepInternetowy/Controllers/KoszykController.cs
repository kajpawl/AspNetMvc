﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepInternetowy.Controllers
{
    public class KoszykController : Controller
    {
        // GET: Koszyk
        public ActionResult Index()
        {
            return View();
        }

        // Dodaj do koszyka
        public ActionResult DodajDoKoszyka(string id)
        {
            return View();
        }
    }
}