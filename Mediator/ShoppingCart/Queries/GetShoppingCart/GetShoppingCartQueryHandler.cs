using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCart>
    {
        private readonly ApplicationDbContext dbContext;
        public GetShoppingCartQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCart> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
            => await dbContext
                .ShoppingCarts
                .Include(sc => sc.User)
                .Include(sc => sc.Order)
                .FirstOrDefaultAsync(
                    sc =>
                        sc.User.Id == query.UserId &&
                        sc.Order == null,
                    cancellationToken
                );
    }
}
