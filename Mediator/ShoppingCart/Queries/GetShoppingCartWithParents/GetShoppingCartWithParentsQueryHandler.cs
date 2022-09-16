using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartWithParentsQueryHandler
        : IRequestHandler<GetShoppingCartWithParentsQuery, ShoppingCart>
    {
        private readonly ApplicationDbContext dbContext;

        public GetShoppingCartWithParentsQueryHandler(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;
        public async Task<ShoppingCart> Handle(GetShoppingCartWithParentsQuery query, CancellationToken cancellationToken)
            => await dbContext
                .ShoppingCarts
                .Include(sc => sc.User)
                .Include(sc => sc.ShoppingCartProducts)
                    .ThenInclude(scp => scp.Product)
                        .ThenInclude(p => p.Images)
                .Include(sc => sc.Order)
                .FirstOrDefaultAsync(
                    sc =>
                        sc.User.Id == query.UserId &&
                        sc.Order == null,
                    cancellationToken
                );
    }
}
