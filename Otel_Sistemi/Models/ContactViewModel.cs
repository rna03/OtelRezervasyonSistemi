namespace Otel_Sistemi.Models
{
    public class ContactViewModel
    {
        public ContactMessage ContactMessage { get; set; } = new ContactMessage
        {
            Ad = string.Empty,
            Email = string.Empty,
            Mesaj = string.Empty
        };

        public HotelInfo? HotelInfo { get; set; }
    }


}
