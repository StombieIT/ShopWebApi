using System;
using System.Collections.Generic;

namespace ShopWebApi.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public User User { get; set; }
        public IList<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public Order Order { get; set; }
    }
}
