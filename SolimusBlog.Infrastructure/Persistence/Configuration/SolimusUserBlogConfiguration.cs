using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolimusBlog.Domain.Entities;

namespace SolimusBlog.Infrastructure.Persistence.Configuration;

public class SolimusUserBlogConfiguration : IEntityTypeConfiguration<SolimusUserBlog>
{
    public void Configure(EntityTypeBuilder<SolimusUserBlog> builder)
    {
        builder.ToTable("SolimusUserBlogs");

        builder.HasOne(ub => ub.SolimusUser)
            .WithMany(u => u.SolimusUserBlogs)
            .HasForeignKey(ub => ub.UserId);
        
        builder.HasOne(ub => ub.Blog)
            .WithMany(b => b.SolimusUserBlogs)
            .HasForeignKey(ub => ub.BlogId);
    }
}