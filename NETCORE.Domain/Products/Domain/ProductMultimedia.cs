using System.ComponentModel.DataAnnotations;

namespace NETCORE.Domain.Products.Domain
{
    public class ProductMultimedia
    {
        [Required]
        [Range(0,100)]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        //[Required]
        //public List<IFormFile> Images { get; set; }
    }
}
