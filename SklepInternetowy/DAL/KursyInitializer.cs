using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SklepInternetowy.Models;
using SklepInternetowy.Migrations;
using System.Data.Entity.Migrations;

namespace SklepInternetowy.DAL
{
    public class KursyInitializer : MigrateDatabaseToLatestVersion<KursyContext, Configuration>
    {
        public static void SeedKursyData(KursyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId=1, NazwaKategorii="asp", NazwaPlikuIkony="asp.png", OpisKategorii="opis asp net mvc" },
                new Kategoria() { KategoriaId=2, NazwaKategorii="java", NazwaPlikuIkony="java.png", OpisKategorii="opis java" },
                new Kategoria() { KategoriaId=3, NazwaKategorii="php", NazwaPlikuIkony="php.png", OpisKategorii="opis php" },
                new Kategoria() { KategoriaId=4, NazwaKategorii="html", NazwaPlikuIkony="html.png", OpisKategorii="opis hml" },
                new Kategoria() { KategoriaId=5, NazwaKategorii="css", NazwaPlikuIkony="css.png", OpisKategorii="opis css" },
                new Kategoria() { KategoriaId=6, NazwaKategorii="xml", NazwaPlikuIkony="xml.png", OpisKategorii="opis xml" },
                new Kategoria() { KategoriaId=7, NazwaKategorii="c#", NazwaPlikuIkony="c#.png", OpisKategorii="opis c#" },
            };

            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            var kursy = new List<Kurs>
            {
                new Kurs() { AutorKursu="tomek", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=99, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="krzysiek", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=199, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="kasia", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=299, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="szymon", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=399, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="łukasz", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=499, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="monika", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=599, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
                new Kurs() { AutorKursu="robert", TytulKursu="asp.net mvc", KategoriaId=1, CenaKursu=699, Bestseller=true, NazwaPlikuObrazka="asp.png", DataDodania=DateTime.Now, OpisKursu="opis kursu" },
            };
            kursy.ForEach(k => context.Kursy.AddOrUpdate(k));
            context.SaveChanges();
        }
    }
}