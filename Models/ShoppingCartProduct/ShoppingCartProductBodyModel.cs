using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ShoppingCartProductBodyModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Количество не может быть меньше 1")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Идентификатор обязателен к указанию")]
        public Guid ProductId { get; set; }
    }
}
