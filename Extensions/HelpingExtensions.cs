using Microsoft.EntityFrameworkCore;
using ShopWebApi.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace ShopWebApi.Extensions
{
    public static class HelpingExtensions
    {
        public static Guid? GetId(this ClaimsPrincipal User)
        {
            Guid id;
            if (Guid.TryParse(User.FindFirstValue("Id"), out id))
                return id;
            return null;
        }
        public static User GetUser(this IRepository repository, ClaimsPrincipal User)
        {
            Guid? userId = User.GetId();
            return repository
                .CreateQuery<User>()
                .Include(u => u.ShoppingCarts)
                    .ThenInclude(sc => sc.ShoppingCartProducts)
                        .ThenInclude(scp => scp.Product)
                .FirstOrDefault(u => u.Id == userId);
        }
    }
}
