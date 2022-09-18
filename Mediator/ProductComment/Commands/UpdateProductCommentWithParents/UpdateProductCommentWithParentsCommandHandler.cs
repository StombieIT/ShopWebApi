using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class UpdateProductCommentWithParentsCommandHandler
        : IRequestHandler<UpdateProductCommentWithParentsCommand, Comment<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public UpdateProductCommentWithParentsCommandHandler(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;
        public async Task<Comment<Product>> Handle(UpdateProductCommentWithParentsCommand command, CancellationToken cancellationToken)
        {

            Comment<Product> productComment = await dbContext
                .ProductComments
                .Include(pc => pc.Author)
                .FirstOrDefaultAsync(
                    pc => pc.Id == command.ProductCommentId,
                    cancellationToken
                );
            if (productComment == null)
                throw new NotFoundException($"Product comment with id '{command.ProductCommentId}' was not found");
            productComment.Text = command.Text;
            productComment.Rating = command.Rating;
            productComment.UpdateDate = DateTime.Now;
            dbContext.Update(productComment);
            await dbContext.SaveChangesAsync(cancellationToken);
            return productComment;

        }
    }
}
