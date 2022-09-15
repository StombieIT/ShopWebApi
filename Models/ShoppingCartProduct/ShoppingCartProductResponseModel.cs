using Microsoft.AspNetCore.Http;
using System;

namespace ShopWebApi.Models
{
    public class ShoppingCartProductResponseModel
    {
        public  Guid Id { get; set; }
        public int Count { get; set; }
        public ProductResponseModel Product { get; set; }
        public DateTime CreationDate { get; set; }
        public ShoppingCartProductResponseModel(ShoppingCartProduct shoppingCartProduct, HttpRequest request)
        {
            Id = shoppingCartProduct.Id;
            Count = shoppingCartProduct.Count;
            Product = new ProductResponseModel(shoppingCartProduct.Product, request);
            CreationDate = shoppingCartProduct.CreationDate;
        }
    }
}
