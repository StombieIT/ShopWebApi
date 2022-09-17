using MediatR;
using ShopWebApi.Models;
using System.Linq;

namespace ShopWebApi.Mediator
{
    public class GetProductsWithImagesQuery
        : IRequest<IQueryable<Product>>
    {}
}
