using System.ComponentModel.DataAnnotations;

namespace Otel_Sistemi.Models
{
    public class ReservationViewModel
    {
        [Required]
        public DateTime? GirisTarihi { get; set; }

        [Required]
        public DateTime? CikisTarihi { get; set; }

        [Required]
        [Range(1, 10)]
        public int OdaSayisi { get; set; }

        [Required]
        [Range(1, 10)]
        public int YetiskinSayisi { get; set; }

        [Required]
        [Range(1, 2)]
        public int CocukSayisi { get; set; }
    }
}
