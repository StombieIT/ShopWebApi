using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartQuery : IRequest<ShoppingCart>
    {
        public Guid UserId { get; set; }
        public GetShoppingCartQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
