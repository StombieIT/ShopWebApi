using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetProductQueryHandler
        : IRequestHandler<GetProductQuery, Product>
    {
        private readonly ApplicationDbContext dbContext;
        public GetProductQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> Handle(GetProductQuery query, CancellationToken cancellationToken)
            => await dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken);
    }
}
