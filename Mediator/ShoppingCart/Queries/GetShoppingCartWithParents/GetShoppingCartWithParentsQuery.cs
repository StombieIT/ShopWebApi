using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartWithParentsQuery
        : IRequest<ShoppingCart>
    {
        public Guid UserId { get; set; }
        public GetShoppingCartWithParentsQuery()
        {}
        public GetShoppingCartWithParentsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
