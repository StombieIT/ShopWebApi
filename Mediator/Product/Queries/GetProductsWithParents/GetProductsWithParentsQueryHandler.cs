using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetProductsWithParentsQueryHandler
        : IRequestHandler<GetProductsWithParentsQuery, IQueryable<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductsWithParentsQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IQueryable<Product>> Handle(GetProductsWithParentsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(
                dbContext
                .Products
                .Include(p => p.Images)
                .Include(p => p.Comments)
                .AsQueryable()
            );
    }
}
