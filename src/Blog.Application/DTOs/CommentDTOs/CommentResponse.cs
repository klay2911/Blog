namespace Blog.Application.DTOs.CommentDTOs
{
    public class CommentResponse
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
