using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebApi.Models
{
    public class ProductResponseWithCommentsModel : ProductResponseModel
    {

        public IEnumerable<ProductCommentResponseModel> Comments { get; set; }
        public ProductResponseWithCommentsModel(Product product, HttpRequest request) : base(product, request)
        {
            Comments = product.Comments.Select(c => new ProductCommentResponseModel(c, request));
        }
    }
}
