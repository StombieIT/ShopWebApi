using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using ShopWebApi.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductsWithParentsQueryHandler
        : IRequestHandler<GetShoppingCartProductsWithParentsQuery, IQueryable<ShoppingCartProduct>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetShoppingCartProductsWithParentsQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<IQueryable<ShoppingCartProduct>> Handle(GetShoppingCartProductsWithParentsQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                    .ThenInclude(sc => sc.User)
                .Include(scp => scp.Product)
                    .ThenInclude(p => p.Images)
                .Where(scp => scp.ShoppingCart.Id == query.ShoppingCartId));
        }
    }
}
