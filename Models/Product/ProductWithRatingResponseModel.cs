using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ShopWebApi.Models
{
    public class ProductWithRatingResponseModel
        : ProductResponseModel
    {
        public decimal? Rating { get; set; }
        public ProductWithRatingResponseModel(Product product, HttpRequest request)
            : base(product, request)
        {
            if (product.Comments.Any())
                Rating = (decimal)product.Comments.Sum(c => c.Rating) / product.Comments.Count();
            else
                Rating = null;
        }
    }
}
