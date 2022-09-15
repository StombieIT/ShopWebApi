using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class RemoveProductCommentHandler
        : IRequestHandler<RemoveProductCommentCommand>
    {
        private readonly ApplicationDbContext dbContext;

        public RemoveProductCommentHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Unit> Handle(RemoveProductCommentCommand command, CancellationToken cancellationToken)
        {
            Comment<Product> productComment
                = await dbContext
                    .ProductComments
                    .FirstOrDefaultAsync(
                        pc =>
                            pc.Id == command.ProductCommentId,
                        cancellationToken
                    );
            if (productComment == null)
                throw new NotFoundException($"Product comment with id '{command.ProductCommentId}' was not found");
            dbContext.Remove(productComment);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
