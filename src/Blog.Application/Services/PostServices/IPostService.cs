using Blog.Application.Common.Paging;
using Blog.Application.DTOs.PostDTOs;

namespace Blog.Application.Services.PostServices
{
    public interface IPostService
    {
        Task<PostResponse> GetByIdAsync(int id);

        Task<PaginationResponse<PostResponse>> GetFilterAsync(PostFilterRequest request);

        Task<PostResponse> CreateAsync(PostDTO dto, string createName, int posterId);

        Task<PostResponse> UpdateAsync(int id, PostDTO dto);

        Task DeleteAsync(int id);
    }
}
