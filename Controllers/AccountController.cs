using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopWebApi.Extensions;
using ShopWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using MediatR;
using ShopWebApi.Mediator;
using ShopWebApi.Exceptions;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IRepository repository;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public AccountController(
            IRepository repository,
            IConfiguration configuration,
            IMediator mediator
        )
        {
            this.repository = repository;
            this.configuration = configuration;
            this.mediator = mediator;
        }
        [HttpGet]
        [Route("/api/[controller]")]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = await mediator.Send(new GetUserByClaimsPrincipalQuery(User));
                return Ok(new UserResponseModel(user, Request));
            }
            return Unauthorized();
        }
        public async Task<IActionResult> Register([FromBody] UserBodyModel model)
        {
            User user = await mediator.Send(new GetUserByLoginQuery(model.Login));
            if (user != null)
            {
                ModelState.AddModelError(model.Login.ToString(), "Пользователь уже найден");
                return BadRequest(ModelState);
            }
            user = await mediator.Send(new AddUserCommand(model.Login, model.Password));
            await AuthenticateAsync(user);
            return Ok(new UserResponseModel(user, Request));
        }
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserBodyModel model)
        {
            User user =
                await mediator.Send(new GetUserByLoginPasswordQuery(model.Login, model.Password));
            if (user == null)
            {
                ModelState.AddModelError(model.Login, "Пользователь не найден");
                return BadRequest(ModelState);
            }
            await AuthenticateAsync(user);
            return Ok(new UserResponseModel(user, Request));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult GetToken()
        {
            User user = repository.GetUser(User);
            var jwt = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("DefaultIssuer"),
                audience: configuration.GetValue<string>("DefaultAudience"),
                claims: new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim("login", user.Login)
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret"))),
                    SecurityAlgorithms.HmacSha256
                )
            );
            return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt) });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Secret() => Ok(new { Login = User.FindFirstValue("login") });
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        private async Task AuthenticateAsync(User user)
        {
            Claim[] claims =
            {
                new Claim("Id", user.Id.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
