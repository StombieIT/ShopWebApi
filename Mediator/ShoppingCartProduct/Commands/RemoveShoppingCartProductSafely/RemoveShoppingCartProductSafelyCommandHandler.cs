using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class RemoveShoppingCartProductSafelyCommandHandler
        : IRequestHandler<RemoveShoppingCartProductSafelyCommand>
    {
        private readonly ApplicationDbContext dbContext;
        public RemoveShoppingCartProductSafelyCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Unit> Handle(RemoveShoppingCartProductSafelyCommand command, CancellationToken cancellationToken)
        {
            ShoppingCartProduct shoppingCartProduct = await dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                    .ThenInclude(scp => scp.ShoppingCartProducts)
                .FirstOrDefaultAsync(scp => scp.Id == command.ShoppingCartProductId, cancellationToken);
            if (shoppingCartProduct == null)
                throw new NotFoundException($"Shopping cart product with id '{command.ShoppingCartProductId}' was not found");
            if (shoppingCartProduct.ShoppingCart.ShoppingCartProducts.Count == 1)
                dbContext.Remove(shoppingCartProduct.ShoppingCart);
            else
                dbContext.Remove(shoppingCartProduct);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
