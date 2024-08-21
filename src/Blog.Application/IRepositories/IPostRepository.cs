using Blog.Application.Common.Paging;
using Blog.Domain.Entities;

namespace Blog.Application.IRepositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<PaginationResponse<Post>> GetFilterAsync (PostFilterRequest request);
    }
}
