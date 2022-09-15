using MediatR;
using ShopWebApi.Models;
using System;
using System.Linq;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductsWithParentsQuery : IRequest<IQueryable<ShoppingCartProduct>>
    {
        public Guid ShoppingCartId { get; set; }
        public GetShoppingCartProductsWithParentsQuery(Guid shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }
        public GetShoppingCartProductsWithParentsQuery()
        {}
    }
}
