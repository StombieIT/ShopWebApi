using System;
using System.ComponentModel.DataAnnotations;

namespace ShopWebApi.Models
{
    public class ProductCommentBodyModel
    {
        [Required(ErrorMessage = "Идентификатор товара обязателен к указанию")]
        public Guid? ProductId { get; set; }
        [Required(ErrorMessage = "Текст обязателен к указанию")]
        public string Text { get; set; }
    }
}
