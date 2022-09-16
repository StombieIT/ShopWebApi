using Microsoft.AspNetCore.Http;
using System;

namespace ShopWebApi.Models
{
    public class ImageResponseModel<TEntity>
        where TEntity : class
    {
        public Guid Id { get; private set; }
        public string Link { get; private set; }
        public ImageResponseModel(Image<TEntity> image, HttpRequest request)
        {
            Id = image.Id;
            Link = $"{request.Scheme}://{request.Host.Value.ToString()}/Images/{image.FileName}";
        }
    }
}
