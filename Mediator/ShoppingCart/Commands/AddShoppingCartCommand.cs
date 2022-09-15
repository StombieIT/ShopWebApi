using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class AddShoppingCartCommand : IRequest<ShoppingCart>
    {
        public Guid UserId { get; set; }
        public AddShoppingCartCommand()
        {}
        public AddShoppingCartCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
