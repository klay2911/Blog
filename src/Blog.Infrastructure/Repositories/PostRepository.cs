using Blog.Application.Common.Paging;
using Blog.Application.IRepositories;
using Blog.Domain.Entities;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
        }

        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _table.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted != true);
        }
        public async Task<PaginationResponse<Post>> GetFilterAsync (PostFilterRequest request)
        {
            IQueryable<Post> query = _table.Where(p => p.IsDeleted != true).Include(p => p.Comments);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p => p.Title.Contains(request.SearchTerm) || p.Content.Contains(request.SearchTerm));
            }
            var totalCount = await query.CountAsync();
            var items = await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).AsNoTracking().ToListAsync();
            return new(items, totalCount);
        }

    }
}
