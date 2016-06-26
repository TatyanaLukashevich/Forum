namespace ORM
{
    using System.Data.Entity;

    public partial class ForumDBContext : DbContext
    {
        public ForumDBContext()
            : base("name=Forum")
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostMessages)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(p => p.Posts);
            modelBuilder.Entity<Section>()
                .HasMany(t => t.Posts);
        }
    }
}
