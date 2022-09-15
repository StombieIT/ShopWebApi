using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductQuery : IRequest<ShoppingCartProduct>
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public GetShoppingCartProductQuery()
        {}
        public GetShoppingCartProductQuery(Guid shoppingCartId, Guid productId)
        {
            ProductId = productId;
            ShoppingCartId = shoppingCartId;
        }
    }
}
