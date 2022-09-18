using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetProductWithImagesQuery
        : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public GetProductWithImagesQuery()
        {}
        public GetProductWithImagesQuery(Guid productId)
            => ProductId = productId;
    }
}
