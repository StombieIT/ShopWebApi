using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetProductCommentWithParentsQuery
        : IRequest<Comment<Product>>
    {
        public Guid ProductCommentId { get; set; }
        public GetProductCommentWithParentsQuery()
        {}
        public GetProductCommentWithParentsQuery(Guid productCommentId)
        {
            ProductCommentId = productCommentId;
        }
    }
}
