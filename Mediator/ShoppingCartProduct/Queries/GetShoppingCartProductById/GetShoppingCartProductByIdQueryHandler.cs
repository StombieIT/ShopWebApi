using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductByIdQueryHandler
        : IRequestHandler<GetShoppingCartProductByIdQuery, ShoppingCartProduct>
    {
        private readonly ApplicationDbContext dbContext;
        public GetShoppingCartProductByIdQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCartProduct> Handle(GetShoppingCartProductByIdQuery query, CancellationToken cancellationToken)
            => await dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                    .ThenInclude(sc => sc.User)
                .Include(scp => scp.Product)
                .FirstOrDefaultAsync(scp => scp.Id == query.Id, cancellationToken);
    }
}
