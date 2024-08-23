using Blog.Application.DTOs.CommentDTOs;
using Blog.Application.Services.CommentServices;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CommentDTO commentDto)
        {
            var comment = await _commentService.CreateCommentAsync(commentDto);
            return Ok(comment);
        }
    }
}
