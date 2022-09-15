using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class AddShoppingCartProductCommand : IRequest<ShoppingCartProduct>
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int Count { get; set; } = 1;
        public AddShoppingCartProductCommand()
        {}
        public AddShoppingCartProductCommand(Guid shoppingCartId, Guid productId)
        {
            ProductId = productId;
            ShoppingCartId = shoppingCartId;
        }
    }
}
