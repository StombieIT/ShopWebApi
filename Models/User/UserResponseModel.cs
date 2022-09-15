using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
        public string AvatarLink { get; set; }
        public UserResponseModel(User user, HttpRequest request)
        {
            Id = user.Id;
            Login = user.Login;
            CreationDate = user.CreationDate;
            AvatarLink = user.AvatarFileName != null
                ? $"{request.Scheme}://{request.Host.Value.ToString()}/Images/{user.AvatarFileName}"
                : null;
        }
    }
}
