using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SklepInternetowy.Models
{
    public class Zamowienie
    {
        public int ZamowienieID { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Wprowadź ulicę")]
        [StringLength(100)]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string KodPocztowy { get; set; }

        [Required(ErrorMessage = "Wprowadź numer telefonu")]
        [StringLength(30)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny numer telefonu.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Wprowadź numer swój adres e-mail")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]

        public string Email { get; set; }

        public string Komentarz { get; set; }

        public DateTime DataDodania { get; set; }

        public StanZamowienia StanZamowienia { get; set; }

        public decimal WartoscZamowienia { get; set; }

        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }

    public enum StanZamowienia
    {
        Nowe,
        Zrealizowane
    }
}