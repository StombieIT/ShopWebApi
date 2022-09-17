using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Extensions;
using ShopWebApi.Mediator;
using ShopWebApi.Models;
using ShopWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private const int pageLimit = 30;
        private readonly IRepository repository;
        private readonly IMediator mediator;
        public ShoppingCartController(IRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }
        [HttpGet]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Index()
        {
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет созданной корзины");
                return BadRequest(ModelState);
            }
            return Ok(new ShoppingCartResponseModel(shoppingCart, Request));
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart != null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь уже имеет созданную корзину");
                return BadRequest(ModelState);
            }
            shoppingCart = await mediator.Send(new AddShoppingCartCommand(user.Id));
            return Ok(new ShoppingCartResponseModel(shoppingCart, Request));
        }
        [HttpGet]
        public async Task<IActionResult> Products(int page = 1, int limit = 6)
        {
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет созданной корзины");
                return BadRequest(ModelState);
            }
            IQueryable<ShoppingCartProduct> shoppingCartProducts
                = await mediator.Send(new GetShoppingCartProductsWithParentsQuery(shoppingCart.Id));
            IPaginator<ShoppingCartProductResponseModel> paginator = new Paginator<ShoppingCartProductResponseModel>(
                page,
                limit,
                shoppingCartProducts.Select(scp => new ShoppingCartProductResponseModel(scp, Request))
            );
            return Ok(paginator);
        }
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddProduct(Guid productId)
        {
            Product product = await mediator.Send(new GetProductQuery(productId));
            if (product == null)
            {
                ModelState.AddModelError(productId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart == null)
                shoppingCart = await mediator.Send(new AddShoppingCartCommand(user.Id));
            ShoppingCartProduct shoppingCartProduct = await mediator.Send(new GetShoppingCartProductQuery(shoppingCart.Id, product.Id));
            if (shoppingCartProduct != null)
            {
                ModelState.AddModelError(shoppingCartProduct.Id.ToString(), "Товар в корзине уже существует");
                return BadRequest(ModelState);
            }
            shoppingCartProduct = await mediator.Send(new AddShoppingCartProductCommand(shoppingCart.Id, product.Id));
            return Ok(new ShoppingCartProductResponseModel(shoppingCartProduct, Request));
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            Product product = await mediator.Send(new GetProductQuery(productId));
            if (product == null)
            {
                ModelState.AddModelError(productId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет созданной корзины");
                return BadRequest(ModelState);
            }
            ShoppingCartProduct shoppingCartProduct =
                await mediator.Send(new GetShoppingCartProductQuery(shoppingCart.Id, product.Id));
            if (shoppingCartProduct == null)
            {
                ModelState.AddModelError(productId.ToString(), "Корзина не содержит товара");
                return BadRequest(ModelState);
            }
            await mediator.Send(new RemoveShoppingCartProductSafelyCommand(shoppingCartProduct.Id));
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ShoppingCartProductBodyModel model)
        {
            Product product = await mediator.Send(new GetProductQuery(model.ProductId));
            if (product == null)
            {
                ModelState.AddModelError(model.ProductId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = await mediator.Send(new GetShoppingCartQuery(user.Id));
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет созданной корзины");
                return BadRequest(ModelState);
            }
            ShoppingCartProduct shoppingCartProduct =
                await mediator.Send(new GetShoppingCartProductQuery(shoppingCart.Id, product.Id));
            if (shoppingCartProduct == null)
            {
                ModelState.AddModelError(model.ProductId.ToString(), "Корзина не содержит товара");
                return BadRequest(ModelState);
            }
            shoppingCartProduct = await mediator.Send(new UpdateShoppingCartProductCommand(shoppingCartProduct.Id, model.Count));
            return Ok(new ShoppingCartProductResponseModel(shoppingCartProduct, Request));
        }
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderBodyModel model)
        {
            User user = repository.GetUser(User);
            ShoppingCart shoppingCart = repository
                .CreateQuery<ShoppingCart>()
                .Include(sc => sc.Order)
                .Include(sc => sc.User)
                .Include(sc => sc.ShoppingCartProducts)
                .FirstOrDefault(sc => sc.Order == null && sc.User.Id == user.Id);
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет подходящей корзины");
                return BadRequest(ModelState);
            }
            if (shoppingCart.ShoppingCartProducts == null)
            {
                ModelState.AddModelError(shoppingCart.Id.ToString(), "Корзина пуста");
                return BadRequest(ModelState);
            }
            Order order = new Order(model) { ShoppingCart = shoppingCart };
            repository.Add(order);
            return Ok(new OrderResponseModel(order, Request));
        }
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            User user
                = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart =
                await mediator.Send(new GetShoppingCartWithParentsQuery(user.Id));
            if (shoppingCart == null)
            {
                ModelState.AddModelError(user.Id.ToString(), "Пользователь не имеет созданной корзины");
                return BadRequest(ModelState);
            }
            return Ok(new ShoppingCartResponseWithInfoModel(shoppingCart, Request));
        }
    }
}
