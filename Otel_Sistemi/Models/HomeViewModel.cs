namespace Otel_Sistemi.Models
{
    public class HomeViewModel
    {
        public ReservationViewModel ReservationForm { get; set; }
        public HotelInfo Hotel { get; set; }
        // EKLE: Müsait odaları burada taşıyacağız
        public List<Room> AvailableRooms { get; set; }
        public int GunSayisi { get; set; }
    }


}
