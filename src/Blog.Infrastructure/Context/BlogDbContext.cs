using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            List<Post> posts = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Title = "First Post",
                    Content = "This is the first post content",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Title = "Second Post",
                    Content = "This is the second post content",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now
                }
            };

            modelBuilder.Entity<Post>().HasData(posts);
        }
    }
}
