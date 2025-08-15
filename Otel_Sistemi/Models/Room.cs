using System.ComponentModel.DataAnnotations.Schema;

namespace Otel_Sistemi.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Numara { get; set; }
        public int Kat { get; set; }
        public string OdaTuru { get; set; } // Standart, Deluxe vs.
        public decimal Fiyat { get; set; }
        public int BuyuklukM2 { get; set; }
        public bool CocukKabul { get; set; }
        public int Kapasite { get; set; } // Örnek: 1, 2, 3, 4 kişilik oda

        public bool IlaveYatak { get; set; }

        // Yeni alan:
        public string? ImagePath { get; set; }
        //public ICollection<RoomService>? RoomServices { get; set; }
        [NotMapped]
        public List<RoomService> RoomServices { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }

}
