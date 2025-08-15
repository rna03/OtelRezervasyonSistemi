namespace Otel_Sistemi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }

        public DateTime OdemeTarihi { get; set; }
        public string OdemeYontemi { get; set; } // nakit, kart
        public decimal Tutar { get; set; }

        public Reservation? Reservation { get; set; }
    }

}
