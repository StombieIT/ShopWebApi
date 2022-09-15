using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public int HouseNumber { get; set; }
        public string Wishes { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderResponseModel(Order order, HttpRequest request)
        {
            Id = order.Id;
            Region = order.Region;
            Locality = order.Locality;
            HouseNumber = order.HouseNumber;
            Wishes = order.Wishes;
            CreationDate = order.CreationDate;
        }
    }
}
