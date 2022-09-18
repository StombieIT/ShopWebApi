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
        [Range(1, 5, ErrorMessage = "Значение может быть только целым числом, принадлежащим интервалу [1, 5]")]
        public int Rating { get; set; }
    }
}
