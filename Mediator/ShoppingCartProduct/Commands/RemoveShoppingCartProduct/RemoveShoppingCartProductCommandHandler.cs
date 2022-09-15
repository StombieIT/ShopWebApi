using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class RemoveShoppingCartProductCommandHandler : IRequestHandler<RemoveShoppingCartProductCommand>
    {
        private readonly ApplicationDbContext dbContext;
        public RemoveShoppingCartProductCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Unit> Handle(RemoveShoppingCartProductCommand command, CancellationToken cancellationToken)
        {
            ShoppingCartProduct shoppingCartProduct = await dbContext
                .ShoppingCartProducts
                .FirstOrDefaultAsync(scp => scp.Id == command.ShoppingCartProductId, cancellationToken);
            if (shoppingCartProduct == null)
                throw new NotFoundException($"Shopping cart product with id '{command.ShoppingCartProductId}' was not found");
            dbContext.Remove(shoppingCartProduct);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
