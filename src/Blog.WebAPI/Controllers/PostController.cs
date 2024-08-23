using Blog.Application.Common.Paging;
using Blog.Application.DTOs.PostDTOs;
using Blog.Application.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFilterAsync(PostFilterRequest request)
        {
            var posts = await _postService.GetFilterAsync(request);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostDTO postDto)
        {
            var post = await _postService.CreateAsync(postDto, "Admin", 1);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PostDTO postDto)
        {
            var post = await _postService.UpdateAsync(id, postDto);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _postService.DeleteAsync(id);
            return Ok();
        }
    }
}
