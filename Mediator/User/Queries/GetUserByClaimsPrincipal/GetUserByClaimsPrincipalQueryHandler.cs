using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebApi.Mediator
{
    public class GetUserByClaimsPrincipalQueryHandler : IRequestHandler<GetUserByClaimsPrincipalQuery, User>
    {
        private readonly ApplicationDbContext dbContext;

        public GetUserByClaimsPrincipalQueryHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> Handle(GetUserByClaimsPrincipalQuery query, CancellationToken cancellationToken)
        {
            string claimIdValue = query
                .User
                .FindFirst(c => c.Type == query.ClaimIdType)
                ?.Value;
            Guid userId = Guid.Parse(claimIdValue ?? Guid.Empty.ToString());
            return await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
