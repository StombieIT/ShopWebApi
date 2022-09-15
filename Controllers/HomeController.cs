using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Extensions;
using ShopWebApi.Models;
using ShopWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        private const int pageLimit = 30;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Products(int page = 1, int limit = 6)
        {
            if (limit > pageLimit)
                return BadRequest($"Превышен лимит элементов в запросе ({pageLimit})");
            User user = repository.GetUser(User);
            ShoppingCart shoppingCart = user?.ShoppingCarts?.FirstOrDefault();
            IPaginator<ProductResponseCheckedModel> productPaginator = new Paginator<ProductResponseCheckedModel>(
                page,
                limit,
                repository
                    .CreateQuery<Product>()
                    .Select(p => new ProductResponseCheckedModel(p, Request, shoppingCart))
            );
            return Ok(productPaginator);
        }
        [HttpGet]
        public IActionResult Users(int page = 1, int limit = 6)
        {
            if (limit > pageLimit)
                return BadRequest();
            IPaginator<UserResponseModel> usersPaginator = new Paginator<UserResponseModel>(
                page,
                limit,
                repository
                    .CreateQuery<User>()
                    .Select(u => new UserResponseModel(u, Request))
            );
            return Ok(usersPaginator);
        }
        [HttpGet]
        public IActionResult AddCookie(string key = "key", string value = "value")
        {
            HttpContext.Response.Cookies.Append(key, value);
            return Ok(new { key, value });
        }
    }
}
