using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ProductCommentResponseModel : CommentResponseModel<Product, UserResponseModel>
    {
        public ProductCommentResponseModel(Comment<Product> comment, HttpRequest request) : base(comment)
        {
            Author = new UserResponseModel(comment.Author, request);
        }
    }
}
