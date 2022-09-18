using MediatR;
using ShopWebApi.Models;
using System.Linq;

namespace ShopWebApi.Mediator
{
    public class GetProductsWithParentsQuery
        : IRequest<IQueryable<Product>>
    {}
}
