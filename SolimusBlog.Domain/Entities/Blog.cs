namespace SolimusBlog.Domain.Entities;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public IEnumerable<SolimusUserBlog> SolimusUserBlogs { get; set; } = [];
}