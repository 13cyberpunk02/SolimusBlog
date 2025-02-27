using SolimusBlog.Domain.Entities;
using SolimusBlog.Domain.Interfaces;
using SolimusBlog.Infrastructure.Persistence.Context;

namespace SolimusBlog.Infrastructure.Persistence.Repositories;

public class BlogRepository(SolimusAppContext context) : GenericRepository<Blog>(context), IBlogRepository
{    
}