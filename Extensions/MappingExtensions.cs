using Microsoft.AspNetCore.Http;
using ShopWebApi.Models;

namespace ShopWebApi.Extensions
{
    /*public static class MappingExtensions
    {
        private static string GetBaseUrl(HttpRequest request) => $"{request.Scheme}://{request.Host.Value.ToString()}";
        private static string GetUrlToImages(HttpRequest request) => $"{GetBaseUrl(request)}/Images/";
        public static UserGetModel ToGetModel(this User user, HttpRequest request) => new UserGetModel
        {
            Id = user.Id,
            Login = user.Login,
            CreationDate = user.CreationDate,
            AvatarLink = user.AvatarFileName == null ? null : GetUrlToImages(request) + user.AvatarFileName
        };
        public static ShoppingCartProductGetModel ToGetModel(this ShoppingCartProduct shoppingCartProduct, HttpRequest request) => new ShoppingCartProductGetModel()
        {
            Id = shoppingCartProduct.Id,
            ShoppingCart = shoppingCartProduct.ShoppingCart.ToGetModel(request),
            Product = shoppingCartProduct.Product.ToGetModel(request),
            Count = shoppingCartProduct.Count,
            CreationDate = shoppingCartProduct.CreationDate
        };
        public static ShoppingCartGetModel ToGetModel(this ShoppingCart shoppingCart, HttpRequest request) => new ShoppingCartGetModel()
        {
            Id = shoppingCart.Id,
            User = shoppingCart.User.ToGetModel(request),
            CreationDate = shoppingCart.CreationDate
        };
        public static ProductGetModel ToGetModel(this Product product, HttpRequest request) => new ProductGetModel()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Discount = product.Discount,
            ImageLink = product.ImageFileName == null ? null : GetUrlToImages(request) + product.ImageFileName
        };
    }*/
}
