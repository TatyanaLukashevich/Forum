namespace ORM
{
    using System;
    using System.Collections.Generic;


    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        //public DateTime CreationDate { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
