namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
