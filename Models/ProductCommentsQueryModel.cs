using System;
using System.ComponentModel.DataAnnotations;

namespace ShopWebApi.Models
{
    public class ProductCommentsQueryModel : PageModel
    {
        [Required(ErrorMessage = "Аргумент обязателен к указанию")]
        public Guid? ProductId { get; set; }
    }
}
