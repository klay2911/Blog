using Blog.Application.IRepositories;
using Blog.Domain.Entities;
using Blog.Infrastructure.Context;

namespace Blog.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext context) : base(context)
        {
        }

    }
}
