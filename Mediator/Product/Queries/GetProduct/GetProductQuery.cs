using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetProductQuery
        : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public GetProductQuery()
        {}
        public GetProductQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}
