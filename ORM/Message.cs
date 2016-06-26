namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Body { get; set; }


        public int UserId { get; set; }
        public int PostId { get; set; }
        public int? ReplyId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual Message Reply { get; set; }
    }
}
