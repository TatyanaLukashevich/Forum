using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<PostEntity> UserPost { get; set; }
        public IEnumerable<MessageEntity> UserMessage { get; set; }

        public int RoleId { get; set; }
    }
}