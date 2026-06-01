

using FreerunZufuss.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;
namespace FreerunZufuss.Web.Models


{
    public class Trip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vyplňte titulek")]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte obsah")]
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte popis")]
        public string Description { get; set; } = "";

        // ⭐ RELATION TO PHOTOS
        public List<TripPhoto> Photos { get; set; } = new();
        [NotMapped]
        public List<IFormFile>? UploadedImages { get; set; }
    }
}


