//using FluentValidation;

namespace NETCORE.Domain.Products.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<string> Thumbnails { get; set; }
        public decimal priceOffert { get; set; }
    }

    //public class ProductValidator : AbstractValidator<Product> 
    //{
    //    public ProductValidator()
    //    {
    //        RuleFor(p => p.Title).NotNull().NotEmpty();
    //    }
    //}
}
