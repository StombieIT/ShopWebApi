using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetProductCommentWithParentsQueryHandler
        : IRequestHandler<GetProductCommentWithParentsQuery, Comment<Product>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetProductCommentWithParentsQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment<Product>> Handle(GetProductCommentWithParentsQuery query, CancellationToken cancellationToken)
            => await dbContext
                .ProductComments
                .Include(pc => pc.Author)
                .Include(pc => pc.Object)
                .FirstOrDefaultAsync(pc => pc.Id == query.ProductCommentId, cancellationToken);
    }
}
