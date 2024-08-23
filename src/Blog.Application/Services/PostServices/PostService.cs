using AutoMapper;
using Blog.Application.Common.Paging;
using Blog.Application.DTOs.PostDTOs;
using Blog.Application.IRepositories;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;

namespace Blog.Application.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostResponse> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id) ?? throw new NotFoundException("Post not found.");
            return _mapper.Map<PostResponse>(post);
        }

        public async Task<PaginationResponse<PostResponse>> GetFilterAsync(PostFilterRequest request)
        {
            var posts = await _postRepository.GetFilterAsync(request);
            var dto = _mapper.Map<IEnumerable<PostResponse>>(posts.Data);
            return new PaginationResponse<PostResponse>(dto, posts.TotalCount);
        }

        public async Task<PostResponse> CreateAsync(PostDTO postDto, string createName, int assignedById)
        {
            var post = _mapper.Map<Post>(postDto);
            post.CreatedBy = createName;
            post.CreatedAt = DateTime.Now;

            await _postRepository.InsertAsync(post);
            var dto = _mapper.Map<PostResponse>(post);
            return dto;
        }

        public async Task<PostResponse> UpdateAsync(int id, PostDTO postDto)
        {
            var post = await _postRepository.GetByIdAsync(id) ?? throw new NotFoundException("Post not found.");
            post = _mapper.Map(postDto, post);
            post.ModifiedBy = "Vu";
            post.ModifiedAt = DateTime.Now;

            await _postRepository.UpdateAsync(post);
            var dto = _mapper.Map<PostResponse>(post);
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id) ?? throw new NotFoundException("Post not found.");
            post.IsDeleted = true;
            post.ModifiedAt = DateTime.Now;
            post.ModifiedBy = "Vu";

            await _postRepository.UpdateAsync(post);
        }
    }
}
