using Microsoft.AspNetCore.Http;
using System;

namespace ShopWebApi.Models
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageLink { get; set; }
        public ProductResponseModel()
        {}
        public ProductResponseModel(Product product, HttpRequest request)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            Price = product.Price;
            Discount = product.Discount;
            CreationDate = product.CreationDate;
            ImageLink = product.ImageFileName != null
                ? $"{request.Scheme}://{request.Host.Value.ToString()}/Images/{product.ImageFileName}"
                : null;
        }
    }
}
