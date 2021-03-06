﻿using Microsoft.AspNet.Identity.EntityFramework;
using SklepInternetowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SklepInternetowy.DAL
{
    public class KursyContext : IdentityDbContext<ApplicationUser>
    {
        public KursyContext() : base("KursyContext")
        {
        }

        static KursyContext()
        {
            Database.SetInitializer<KursyContext>(new KursyInitializer());
        }

        public static KursyContext Create()
        {
            return new KursyContext();
        }

        public DbSet<Kurs> Kursy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Wyłacza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }*/
    }
}