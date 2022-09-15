using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ShoppingCartProduct
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
