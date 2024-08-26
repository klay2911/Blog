using Blog.Application.Common.Paging;
using Blog.Application.IRepositories;
using Blog.Domain.Entities;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task<PaginationResponse<Post>> GetFilterAsync(PostFilterRequest request)
        {
            IQueryable<Post> query = _table.Where(p => p.IsDeleted != true).Include(p => p.Comments);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p => p.Title.Contains(request.SearchTerm) || p.Content.Contains(request.SearchTerm));
            }

            var sortProperty = GetSortProperty(request);
            query = request.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(sortProperty)
                : query.OrderBy(sortProperty);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).AsNoTracking().ToListAsync();
            return new PaginationResponse<Post>(items, totalCount);
        }

        private static Expression<Func<Post, object>> GetSortProperty(PostFilterRequest request) =>
            request.SortColumn?.ToLower() switch
            {
                "title" => post => post.Title,
                "content" => post => post.Content,
                "createdat" => post => post.CreatedAt,
                "modifiedat" => post => post.ModifiedAt,
                _ => post => post.CreatedAt
            };
    }
}
