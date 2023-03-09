using System.ComponentModel.DataAnnotations;

namespace NET_WebApp_Backend.Models
{
    public class ProductMultimedia
    {
        [Required]
        [Range(0,100)]
        public int? Id { get; set; }
        
        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }
        
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
