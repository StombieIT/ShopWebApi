using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class AddProductCommentCommand
        : IRequest<Comment<Product>>
    {
        public Guid ProductId { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public AddProductCommentCommand(Guid productId, Guid authorId, string text, int rating)
        {
            ProductId = productId;
            AuthorId = authorId;
            Text = text;
            Rating = rating;
        }
        public AddProductCommentCommand()
        {}
    }
}
