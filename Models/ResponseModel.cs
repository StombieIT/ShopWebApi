using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Models
{
    public class ResponseModel
    {
        public Guid? TargetId { get; private set; }
        public IEnumerable<string> Messages { get; private set; }
        public ResponseModel(Guid? targetId, params string[] messages)
        {
            TargetId = targetId;
            Messages = messages;
        }
    }
}
