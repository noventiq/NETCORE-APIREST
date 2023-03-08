using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NET_WebApp_Backend.Models
{
    public class ProductMultimedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
