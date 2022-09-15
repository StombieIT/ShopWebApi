using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public IList<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public IList<Comment<Product>> Comments { get; set; }
    }
}
