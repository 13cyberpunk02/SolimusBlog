using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SolimusBlog.Application.Models.Configurations;
using SolimusBlog.Domain.Interfaces;
using SolimusBlog.Infrastructure.Persistence.Context;
using SolimusBlog.Infrastructure.Persistence.Repositories;

namespace SolimusBlog.Infrastructure.ServiceCollection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnection = configuration["ConnectionStrings:PostgresConnection"];
        services.AddDbContext<SolimusAppContext>(options => options.UseNpgsql(postgresConnection));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBlogRepository, BlogRepository>();
        return services;
    }
}