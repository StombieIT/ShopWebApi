using MediatR;
using ShopWebApi.Models;
using System;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductByIdQuery : IRequest<ShoppingCartProduct>
    {
        public Guid Id { get; set; }
        public GetShoppingCartProductByIdQuery()
        {}
        public GetShoppingCartProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
