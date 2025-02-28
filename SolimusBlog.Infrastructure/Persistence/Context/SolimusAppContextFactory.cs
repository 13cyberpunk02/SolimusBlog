using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SolimusBlog.Infrastructure.Persistence.Configuration;

namespace SolimusBlog.Infrastructure.Persistence.Context;

public class SolimusAppContextFactory : IDesignTimeDbContextFactory<SolimusAppContext>
{
    public SolimusAppContext CreateDbContext(string[] args)
    {
        var configuration = AppConfiguration.LoadConfiguration();
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<SolimusAppContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new SolimusAppContext(optionsBuilder.Options);
    }
}