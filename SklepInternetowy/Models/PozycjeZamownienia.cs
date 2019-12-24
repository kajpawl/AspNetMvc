namespace SklepInternetowy.Models
{
    public class PozycjeZamownienia
    {
        public int PozycjaZamowniemiaId { get; set; }
        public int ZamowienieID { get; set; }
        public int KursId { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaZakupu { get; set; }

        public virtual Kurs kurs { get; set; }
        public virtual Zamowienie zamowienie { get; set; }
    }
}