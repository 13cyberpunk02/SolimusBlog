using SolimusBlog.Domain.Interfaces;
using SolimusBlog.Infrastructure.Persistence.Context;

namespace SolimusBlog.Infrastructure.Persistence.Repositories;

public class UnitOfWork(SolimusAppContext context) : IUnitOfWork
{
    public async Task<bool> CommitAsync()
    {
        var result = await context.SaveChangesAsync();
        return result > 0;
    }
}