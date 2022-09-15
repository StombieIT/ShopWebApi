using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, User>
    {
        private readonly ApplicationDbContext dbContext;

        public GetUserByLoginQueryHandler(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public async Task<User> Handle(GetUserByLoginQuery query, CancellationToken cancellationToken)
            => await dbContext
                .Users
                .FirstOrDefaultAsync(
                    u => u.Login == query.Login,
                    cancellationToken
                );
    }
}
