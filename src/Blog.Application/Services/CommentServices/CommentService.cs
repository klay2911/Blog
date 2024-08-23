using AutoMapper;
using Blog.Application.DTOs.CommentDTOs;
using Blog.Application.IRepositories;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;

namespace Blog.Application.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentDTO commentCreateDto)
        {
            var comment = new Comment
            {
                Content = commentCreateDto.Content,
                PostId = commentCreateDto.PostId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Commenter"
            };

            await _commentRepository.InsertAsync(comment);
            await _commentRepository.SaveChangeAsync();

            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id) ?? throw new NotFoundException("Comment not found.");
            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetByIdAsync(postId) ?? throw new NotFoundException("Comment not found.");
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
    }
}
