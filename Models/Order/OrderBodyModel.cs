using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class OrderBodyModel
    {
        [Required(ErrorMessage = "Регион обязателен к указанию")]
        [MaxLength(256, ErrorMessage = "Длина региона не должна превышать 256 символов")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Населённый пункт обязателен к указанию")]
        [MaxLength(128, ErrorMessage = "Длина населённого пункта не должна превышать 128 символов")]
        public string Locality { get; set; }
        [Required(ErrorMessage = "Номер дома обязателен к указанию")]
        [Range(1, double.MaxValue, ErrorMessage = "Номер дома не может быть меньше 1")]
        public int HouseNumber { get; set; }
        [MaxLength(1024, ErrorMessage = "Длина населённого пункта не должна превышать 1024 символа")]
        
        public string Wishes { get; set; }
    }
}
