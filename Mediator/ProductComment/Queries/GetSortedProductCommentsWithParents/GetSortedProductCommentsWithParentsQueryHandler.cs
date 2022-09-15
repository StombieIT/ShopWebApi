using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetSortedProductCommentsWithParentsQueryHandler
        : IRequestHandler<GetSortedProductCommentsWithParentsQuery, IQueryable<Comment<Product>>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetSortedProductCommentsWithParentsQueryHandler(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;
        public Task<IQueryable<Comment<Product>>> Handle(GetSortedProductCommentsWithParentsQuery query, CancellationToken cancellationToken)
            => Task.FromResult(
                dbContext
                    .ProductComments
                    .Include(c => c.Author)
                    .Include(c => c.Object)
                    .Where(c => c.Object.Id == query.ObjectId)
                    .OrderByDescending(c => c.Author.Id == query.AuthorId)
                    .ThenByDescending(c => c.CreationDate)
                    .AsQueryable()
            );

            
    }
}
