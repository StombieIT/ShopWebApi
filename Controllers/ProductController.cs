using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Extensions;
using ShopWebApi.Mediator;
using ShopWebApi.Models;
using ShopWebApi.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IRepository repository;
        private readonly IMediator mediator;

        public ProductController(IRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }
        [HttpGet]
        [Route("/api/Products")]
        public async Task<IActionResult> Index(int page = 1, int limit = 6)
        {
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            ShoppingCart shoppingCart = user?.ShoppingCarts?.FirstOrDefault();
            IPaginator<ProductResponseCheckedModel> productPaginator = new Paginator<ProductResponseCheckedModel>(
                page,
                limit,
                repository
                    .CreateQuery<Product>()
                    .Include(p => p.Images)
                    .Select(p => new ProductResponseCheckedModel(p, Request, shoppingCart))
            );
            return Ok(productPaginator);
        }
        [Route("/api/[controller]/{productId}")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid productId)
        {
            Product product = await mediator.Send(new GetProductWithImagesQuery(productId));
            if (product == null)
            {
                ModelState.AddModelError(productId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            return Ok(new ProductResponseModel(product, Request));
        }
        [HttpGet]
        public async Task<IActionResult> Comments([FromQuery] ProductCommentsQueryModel model)
        {
            Product product
                = await mediator.Send(new GetProductQuery((Guid)model.ProductId));
            if (product == null)
            {
                ModelState.AddModelError(model.ProductId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            IQueryable<Comment<Product>> productComments
                = await mediator.Send(new GetSortedProductCommentsWithParentsQuery(
                    product.Id,
                    user?.Id ?? Guid.Empty
                ));
            IPaginator<ProductCommentResponseModel> paginator = new Paginator<ProductCommentResponseModel>(
                model.Page,
                model.Limit,
                productComments
                    .Select(pc => new ProductCommentResponseModel(pc, Request))
            );
            return Ok(paginator);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] ProductCommentBodyModel model)
        {
            Product product = await mediator.Send(new GetProductQuery((Guid)model.ProductId));
            if (product == null)
            {
                ModelState.AddModelError(model.ProductId.ToString(), "Товар не найден");
                return BadRequest(ModelState);
            }
            User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            Comment<Product> productComment =
                await mediator.Send(new AddProductCommentCommand(product.Id, user.Id, model.Text));
            return Ok(new ProductCommentResponseModel(productComment, Request));
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentBodyModel model)
        {
            Comment<Product> productComment =
                await mediator.Send(new GetProductCommentWithParentsQuery((Guid)model.CommentId));
            if (productComment == null)
            {
                ModelState.AddModelError(model.CommentId.ToString(), "Комментарий не найден");
                return BadRequest(ModelState);
            }
            User user
                = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            if (user.Id != productComment.Author.Id)
            {
                ModelState.AddModelError(user.Id.ToString(), "Нет доступа к комментарию");
                return StatusCode(403, ModelState);
            }
            productComment
                = await mediator.Send(new UpdateProductCommentWithParentsCommand(productComment.Id, model.Text));
            return Ok(new ProductCommentResponseModel(productComment, Request));
        }
        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> RemoveComment([Required] Guid? commentId)
        {
            Comment<Product> productComment =
                await mediator.Send(new GetProductCommentWithParentsQuery((Guid)commentId));
            if (productComment == null)
            {
                ModelState.AddModelError(commentId.ToString(), "Комментарий не найден");
                return BadRequest(ModelState);
            }
            User user =
                await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
            if (productComment.Author.Id != user.Id)
            {
                ModelState.AddModelError(commentId.ToString(), "Нет доступа к комментарию");
                return StatusCode(403, ModelState);
            }
            await mediator.Send(new RemoveProductCommentCommand(productComment.Id));
            return Ok();
        }
    }
}
