using System;

namespace ShopWebApi.Models
{
    public class CommentResponseModel<TEntity>
        where TEntity : class
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Rating { get; set; }
        public CommentResponseModel(Comment<TEntity> comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            CreationDate = comment.CreationDate;
            UpdateDate = comment.UpdateDate;
            Rating = comment.Rating;
        }
    }
}
