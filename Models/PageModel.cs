using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class PageModel
    {
        public int Page { get; set; } = 1;
        [Range(1, 30, ErrorMessage = "Аргумент должен принадлежать промежутку [1, 30]")]
        public int Limit { get; set; } = 10;
    }
}
