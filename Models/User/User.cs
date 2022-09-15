using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Role Role { get; set; }
        public string AvatarFileName { get; set; }
        public IList<ShoppingCart> ShoppingCarts { get; set; }
        public IList<Comment<Product>> ProductComments { get; set; }
    }
}
