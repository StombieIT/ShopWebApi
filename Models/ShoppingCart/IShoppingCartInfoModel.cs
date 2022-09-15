using System.Collections.Generic;

namespace ShopWebApi.Models
{
    public interface IShoppingCartInfoModel
    {
        decimal TotalPrice { get; }
        decimal TotalDiscount { get; }
        IEnumerable<ShoppingCartProductResponseModel> ShoppingCartProducts { get; }
    }
}
