using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class CommentResponseModel<TEntity, TUserGetModel>
        where TEntity : class
    {
        public Guid Id { get; set; }
        public TUserGetModel Author { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public CommentResponseModel(Comment<TEntity> comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            CreationDate = comment.CreationDate;
            UpdateDate = comment.UpdateDate;
        }
    }
}
