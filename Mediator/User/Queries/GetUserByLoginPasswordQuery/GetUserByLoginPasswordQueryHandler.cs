using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Exceptions;
using ShopWebApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetUserByLoginPasswordQueryHandler
        : IRequestHandler<GetUserByLoginPasswordQuery, User>
    {
        private readonly ApplicationDbContext dbContext;

        public GetUserByLoginPasswordQueryHandler(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public async Task<User> Handle(GetUserByLoginPasswordQuery query, CancellationToken cancellationToken)
            => await dbContext
                .Users
                .FirstOrDefaultAsync(
                    u =>
                        u.Login == query.Login &&
                        u.Password == query.Password,
                    cancellationToken
                );
    }
}
