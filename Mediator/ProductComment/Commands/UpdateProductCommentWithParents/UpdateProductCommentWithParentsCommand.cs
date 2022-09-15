using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class UpdateProductCommentWithParentsCommand
        : IRequest<Comment<Product>>
    {
        public Guid ProductCommentId { get; set; }
        public string Text { get; set; }
        public UpdateProductCommentWithParentsCommand(Guid productCommentId, string text)
        {
            ProductCommentId = productCommentId;
            Text = text;
        }
        public UpdateProductCommentWithParentsCommand()
        {}
    }
}
