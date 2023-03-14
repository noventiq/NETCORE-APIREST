using FluentValidation;

namespace NETCORE.Domain.Products.DTO
{
    public class RequestProduct
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
        public List<string> Thumbnails { get; set; }
        public RequestVendor? Vendor { get; set; }
    }

    public class RequestVendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RequestProductValidator : AbstractValidator<RequestProduct>
    {
        public RequestProductValidator()
        {
            RuleFor(p => p.Title).NotNull().NotEmpty();
            RuleFor(p => p.Vendor).NotNull().SetValidator(new RequestVendorValidator());
            // RuleFor(p => p.Vendor).NotNull();
            RuleFor(p => p.Thumbnails).NotNull().ForEach(t =>
            {
                t.Empty();
            });
        }
    }

    public class RequestVendorValidator : AbstractValidator<RequestVendor>
    {
        public RequestVendorValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty();
            RuleFor(p => p.Name).NotNull().NotEmpty();
        }
    }
}
