using System;
using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<DalPost> UserPost { get; set; }
        public IEnumerable<DalMessage> UserMessage { get; set; }

        public int RoleId { get; set; }
    }
}