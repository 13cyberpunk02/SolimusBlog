namespace SolimusBlog.Domain.Entities;

public class SolimusUserBlog
{
    public string UserId { get; set; } = string.Empty;
    public int BlogId { get; set; } 
    public Blog Blog { get; set; } = null!;
    public SolimusUser SolimusUser { get; set; } = null!;
}