using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfPost { get; set; }
        public string Body { get; set; }
        public UserViewModel Author { get; set; }
        public string AuthorLogin { get; set; }

        public int UserId { get; set; }
        public int PostId { get; set; }
        public int? ReplyId { get; set; }
    }
}