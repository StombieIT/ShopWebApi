using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;
using System;
using ShopWebApi.Exceptions;

namespace ShopWebApi.Mediator
{
    public class AddShoppingCartCommandHandler : IRequestHandler<AddShoppingCartCommand, ShoppingCart>
    {
        private readonly ApplicationDbContext dbContext;
        public AddShoppingCartCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCart> Handle(AddShoppingCartCommand command, CancellationToken cancellationToken)
        {
            User user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException($"User with id '{command.UserId}' was not found");
            ShoppingCart shoppingCart = await dbContext
                .ShoppingCarts
                .Include(sc => sc.User)
                .Include(sc => sc.Order)
                .FirstOrDefaultAsync(
                    sc =>
                        sc.User.Id == command.UserId &&
                        sc.Order == null
                );
            if (shoppingCart != null)
                throw new AlreadyExistsException($"User with id '${command.UserId}' already has shopping cart");
            shoppingCart = new ShoppingCart
            {
                User = user
            };
            await dbContext.AddAsync(shoppingCart, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return shoppingCart;
        }
    }
}
