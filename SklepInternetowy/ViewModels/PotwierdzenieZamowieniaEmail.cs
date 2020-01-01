using Postal;
using SklepInternetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepInternetowy.ViewModels
{
    public class PotwierdzenieZamowieniaEmail : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public decimal Wartosc { get; set; }
        public int NumerZamowienia { get; set; }
        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }
}