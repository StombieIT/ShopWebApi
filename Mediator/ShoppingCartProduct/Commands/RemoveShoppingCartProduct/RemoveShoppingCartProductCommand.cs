using MediatR;
using System;

namespace ShopWebApi.Mediator
{
    public class RemoveShoppingCartProductCommand : IRequest
    {
        public Guid ShoppingCartProductId { get; set; }
        public RemoveShoppingCartProductCommand()
        {}
        public RemoveShoppingCartProductCommand(Guid shoppingCartProductId)
        {
            ShoppingCartProductId = shoppingCartProductId;
        }
    }
}
