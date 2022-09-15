using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class UpdateShoppingCartProductCommand : IRequest<ShoppingCartProduct>
    {
        public Guid ShoppingCartProductId { get; set; }
        public int Count { get; set; }
        public UpdateShoppingCartProductCommand()
        {}
        public UpdateShoppingCartProductCommand(Guid shoppingCartProductId, int count)
        {
            ShoppingCartProductId = shoppingCartProductId;
            Count = count;
        }
    }
}
