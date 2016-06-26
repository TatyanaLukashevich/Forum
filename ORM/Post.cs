namespace ORM
{
    using System;
    using System.Collections.Generic;

    public partial class Post
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Body { get; set; }

        public int UserID { get; set; }
        public int SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Message> PostMessages { get; set; }

    }
}
