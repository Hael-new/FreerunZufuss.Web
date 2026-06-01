using System.ComponentModel.DataAnnotations;

namespace FreerunZufuss.Web.Models
{
    public class TripPhoto
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; } = "";

        // FK
        public int TripId { get; set; }

        public Trip? Trip { get; set; }
    }
}