using MediatR;
using ShopWebApi.Models;
using System.Security.Claims;

namespace ShopWebApi.Mediator
{
    public class GetUserByClaimsPrincipalQuery : IRequest<User>
    {
        public ClaimsPrincipal User { get; private set; }
        public string ClaimIdType { get; private set; }
        public GetUserByClaimsPrincipalQuery(ClaimsPrincipal user) : this(user, "Id")
        {}
        public GetUserByClaimsPrincipalQuery(ClaimsPrincipal user, string claimIdType)
        {
            User = user;
            ClaimIdType = claimIdType;
        }
    }
}
