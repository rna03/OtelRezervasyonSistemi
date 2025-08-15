namespace Otel_Sistemi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; } // "musteri", "personel", "admin"
        public string? TcKimlikNo { get; set; }


        public ICollection<Reservation>? Reservations { get; set; }
    }

}
