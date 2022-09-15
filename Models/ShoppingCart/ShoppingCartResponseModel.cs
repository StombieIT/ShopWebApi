using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ShoppingCartResponseModel
    {
        protected readonly ShoppingCart shoppingCart;
        protected readonly HttpRequest request;
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public UserResponseModel User { get; set; }
        public ShoppingCartResponseModel(ShoppingCart shoppingCart, HttpRequest request)
        {
            this.shoppingCart = shoppingCart;
            this.request = request;
            Id = shoppingCart.Id;
            CreationDate = shoppingCart.CreationDate;
            User = new UserResponseModel(shoppingCart.User, request);
        }
    }
}
