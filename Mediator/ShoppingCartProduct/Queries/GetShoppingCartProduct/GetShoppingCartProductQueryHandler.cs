using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetShoppingCartProductQueryHandler
        : IRequestHandler<GetShoppingCartProductQuery, ShoppingCartProduct>
    {
        private readonly ApplicationDbContext dbContext;

        public GetShoppingCartProductQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCartProduct> Handle(GetShoppingCartProductQuery query, CancellationToken cancellationToken)
        {
            ShoppingCartProduct shoppingCartProduct = await dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                    .ThenInclude(sc => sc.User)
                .Include(scp => scp.Product)
                .FirstOrDefaultAsync(
                    scp =>
                        scp.Product.Id == query.ProductId &&
                        scp.ShoppingCart.Id == query.ShoppingCartId,
                    cancellationToken
                );
            return shoppingCartProduct;
        }
    }
}
