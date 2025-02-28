using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SolimusBlog.Application.Services;
using SolimusBlog.Application.Services.Interfaces;

namespace SolimusBlog.Application.ServiceCollection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}