using System;
namespace ShopWebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public int HouseNumber { get; set; }
        public string Wishes { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Order()
        {}
        public Order(OrderBodyModel model)
        {
            Region = model.Region;
            Locality = model.Locality;
            HouseNumber = model.HouseNumber;
            Wishes = model.Wishes;
        }

    }
}
