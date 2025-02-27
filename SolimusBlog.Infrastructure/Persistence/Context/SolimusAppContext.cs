using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolimusBlog.Domain.Entities;

namespace SolimusBlog.Infrastructure.Persistence.Context;

public class SolimusAppContext(DbContextOptions<SolimusAppContext> options) : IdentityDbContext<SolimusUser>(options)
{

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<SolimusUserBlog> SolimusUserBlogs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SolimusAppContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}