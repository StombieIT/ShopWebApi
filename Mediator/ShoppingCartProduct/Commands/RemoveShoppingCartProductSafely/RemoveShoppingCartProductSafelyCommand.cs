using MediatR;
using System;

namespace ShopWebApi.Mediator
{
    public class RemoveShoppingCartProductSafelyCommand : IRequest
    {
        public Guid ShoppingCartProductId { get; set; }
        public RemoveShoppingCartProductSafelyCommand()
        { }
        public RemoveShoppingCartProductSafelyCommand(Guid shoppingCartProductId)
        {
            ShoppingCartProductId = shoppingCartProductId;
        }
    }
}
