namespace Otel_Sistemi.Models
{
    public class HomeText
    {
        public int Id { get; set; }
        public string Sayfa { get; set; } // "anasayfa", "iletisim", "hakkimizda"
        public string Baslik { get; set; }
        public string Icerik { get; set; }
    }

}
