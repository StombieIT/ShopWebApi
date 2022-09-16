using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class AddShoppingCartProductCommandHandler : IRequestHandler<AddShoppingCartProductCommand, ShoppingCartProduct>
    {
        private readonly ApplicationDbContext dbContext;
        public AddShoppingCartProductCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCartProduct> Handle(AddShoppingCartProductCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart shoppingCart = await dbContext
                .ShoppingCarts
                .FirstOrDefaultAsync(sc => sc.Id == command.ShoppingCartId, cancellationToken);
            if (shoppingCart == null)
                throw new NotFoundException($"Shopping cart with id '{command.ShoppingCartId}' was not found");
            Product product = await dbContext
                .Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);
            if (product == null)
                throw new NotFoundException($"Product with id '{command.ProductId}' was not found");
            ShoppingCartProduct shoppingCartProduct = await dbContext
                .ShoppingCartProducts
                .Include(scp => scp.ShoppingCart)
                .Include(scp => scp.Product)
                .FirstOrDefaultAsync(
                    scp =>
                        scp.Product.Id == command.ProductId
                        && scp.ShoppingCart.Id == command.ShoppingCartId,
                    cancellationToken
                );
            if (shoppingCartProduct != null)
                throw new AlreadyExistsException($"Shopping cart product with shopping cart id '{command.ShoppingCartId}' and product id '{command.ProductId}' already exists");
            shoppingCartProduct = new ShoppingCartProduct
            {
                ShoppingCart = shoppingCart,
                Product = product,
                Count = command.Count
            };
            await dbContext
                .AddAsync(shoppingCartProduct, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return shoppingCartProduct;
        }
    }
}
