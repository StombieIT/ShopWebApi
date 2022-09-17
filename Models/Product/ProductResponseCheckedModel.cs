using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace ShopWebApi.Models
{
    public class ProductResponseCheckedModel : ProductResponseModel
    {
        public bool IsInShoppingCart { get; set; }
        public ProductResponseCheckedModel(Product product, ShoppingCart shoppingCart, HttpRequest request)
            : base(product, request)
        {
            IsInShoppingCart = shoppingCart
                ?.ShoppingCartProducts
                ?.FirstOrDefault(scp => scp.Product.Id == product.Id)
                != null;
        }
    }
}
