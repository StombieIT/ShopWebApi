using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class UpdateShoppingCartProductCommandHandler : IRequestHandler<UpdateShoppingCartProductCommand, ShoppingCartProduct>
    {
        private readonly ApplicationDbContext dbContext;

        public UpdateShoppingCartProductCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCartProduct> Handle(UpdateShoppingCartProductCommand command, CancellationToken cancellationToken)
        {
            ShoppingCartProduct shoppingCartProduct = await dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                    .ThenInclude(sc => sc.User)
                .Include(scp => scp.Product)
                    .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(
                    scp =>
                        scp.Id == command.ShoppingCartProductId,
                    cancellationToken
                );
            if (shoppingCartProduct == null)
                throw new NotFoundException($"Shopping cart product with id '{command.ShoppingCartProductId}' was not found");
            shoppingCartProduct.Count = command.Count;
            dbContext.Update(shoppingCartProduct);
            await dbContext.SaveChangesAsync(cancellationToken);
            return shoppingCartProduct;
        }
    }
}
