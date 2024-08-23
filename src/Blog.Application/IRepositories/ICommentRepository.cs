using Blog.Application.Common.Paging;
using Blog.Domain.Entities;

namespace Blog.Application.IRepositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
    }
}
