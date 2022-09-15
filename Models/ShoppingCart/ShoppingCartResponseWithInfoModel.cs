using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ShoppingCartResponseWithInfoModel : ShoppingCartResponseModel, IShoppingCartInfoModel
    {
        public decimal TotalPrice => shoppingCart
            .ShoppingCartProducts
            .Select(scp => scp.Product.Price * scp.Count)
            .Sum();
        public decimal TotalDiscount => shoppingCart
            .ShoppingCartProducts
            .Where(scp => scp.Product.Discount != null)
            .Select(scp => (decimal)scp.Product.Discount * scp.Count)
            .Sum();
        public IEnumerable<ShoppingCartProductResponseModel> ShoppingCartProducts => shoppingCart
            .ShoppingCartProducts
            .Select(scp => new ShoppingCartProductResponseModel(scp, request));
        public ShoppingCartResponseWithInfoModel(ShoppingCart shoppingCart, HttpRequest request) : base(shoppingCart, request)
        { }
    }
}
