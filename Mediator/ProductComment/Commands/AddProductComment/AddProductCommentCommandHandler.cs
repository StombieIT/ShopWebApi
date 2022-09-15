using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class AddProductCommentCommandHandler
        : IRequestHandler<AddProductCommentCommand, Comment<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public AddProductCommentCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Comment<Product>> Handle(AddProductCommentCommand command, CancellationToken cancellationToken)
        {
            Product product = await dbContext
                .Products
                .FirstOrDefaultAsync(
                    p =>
                        p.Id == command.ProductId,
                    cancellationToken
                );
            if (product == null)
                throw new NotFoundException($"Product with id '{command.ProductId}' was not found");
            User user = await dbContext
                .Users
                .FirstOrDefaultAsync(
                    u =>
                        u.Id == command.AuthorId,
                    cancellationToken
                );
            if (user == null)
                throw new NotFoundException($"User with id '{command.AuthorId}' was not found");
            Comment<Product> productComment = new Comment<Product>
            {
                Object = product,
                Author = user,
                Text = command.Text
            };
            await dbContext.AddAsync(productComment, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return productComment;

        }
    }
}
