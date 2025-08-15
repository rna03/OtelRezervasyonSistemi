namespace Otel_Sistemi.Models
{
    public class Musteri
    {
        public int Id { get; set; }

        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? TcKimlikNo { get; set; }
        public int RezervasyonId { get; set; } // yeni kolon
                                               // Bu satırı EKLE — bir müşterinin birden fazla rezervasyonu olabilir
        public ICollection<Reservation>? Reservations { get; set; }
    }

}
