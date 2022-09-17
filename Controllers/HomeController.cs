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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class HomeController : Controller
    {
    }
}
