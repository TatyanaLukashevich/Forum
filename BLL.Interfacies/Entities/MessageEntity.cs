using System;

namespace BLL.Interface.Entities
{
   public class MessageEntity
    {
        public int Id { get; set; }
        public DateTime DateOfMessage { get; set; }
        public string Body { get; set; }
        public string AuthorLogin { get; set; }
        public int AuthorId { get; set; }
        public int PostID { get; set; }
        public int? ReplyId { get; set; }
    }
}
