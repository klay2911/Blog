using Blog.Application.DTOs.CommentDTOs;

namespace Blog.Application.Services.CommentServices
{
    public interface ICommentService
    {
        Task<CommentResponse> CreateCommentAsync(CommentDTO commentCreateDto);
        Task<CommentResponse> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentResponse>> GetCommentsByPostIdAsync(int postId);
    }
}
