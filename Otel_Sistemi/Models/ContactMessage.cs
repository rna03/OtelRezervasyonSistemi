using System.ComponentModel.DataAnnotations;

namespace Otel_Sistemi.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public required string Ad { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public required string Mesaj { get; set; }

        public DateTime Tarih { get; set; }
    }

}
