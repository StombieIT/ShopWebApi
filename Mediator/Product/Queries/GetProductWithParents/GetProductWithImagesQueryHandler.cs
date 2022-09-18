using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetProductWithImagesQueryHandler
        : IRequestHandler<GetProductWithImagesQuery, Product>
    {
        private readonly ApplicationDbContext dbContext;
        public GetProductWithImagesQueryHandler(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;
        public async Task<Product> Handle(GetProductWithImagesQuery query, CancellationToken cancellationToken)
            => await dbContext
                .Products
                .Include(p => p.Images)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(
                    p => p.Id == query.ProductId,
                    cancellationToken
                );
                
            
    }
}
