namespace Otel_Sistemi.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int? UserId { get; set; }   // Web'den gelen müşteri
       

        public int RoomId { get; set; }

        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }

        public bool IptalEdildi { get; set; }
        public DateTime? IptalTarihi { get; set; }

        public User? User { get; set; }
        public Musteri? Musteri { get; set; }
        public Room? Room { get; set; }
        public Payment? Payment { get; set; }
    }

}


