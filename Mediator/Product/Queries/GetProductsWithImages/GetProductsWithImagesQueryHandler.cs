using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetProductsWithImagesQueryHandler
        : IRequestHandler<GetProductsWithImagesQuery, IQueryable<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductsWithImagesQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IQueryable<Product>> Handle(GetProductsWithImagesQuery request, CancellationToken cancellationToken)
            => Task.FromResult(
                dbContext
                .Products
                .Include(p => p.Images)
                .AsQueryable()
            );
    }
}
