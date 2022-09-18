using Microsoft.AspNetCore.Http;

namespace ShopWebApi.Models
{
    public class ProductCommentResponseModel : CommentResponseModel<Product>
    {
        public UserResponseModel Author { get; set; }
        public ProductCommentResponseModel(Comment<Product> comment, HttpRequest request)
            : base(comment)
        {
            Author = new UserResponseModel(comment.Author, request);
        }
    }
}
