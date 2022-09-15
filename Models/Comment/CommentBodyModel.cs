using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class CommentBodyModel
    {
        [Required(ErrorMessage = "Аргумент обязателен к указанию")]
        public Guid? CommentId { get; set; }
        [Required(ErrorMessage = "Аргумент обязателен к указанию")]
        public string Text { get; set; }
    }
}
