using Scalar.AspNetCore;
using SolimusBlog.API.Extensions;
using SolimusBlog.Application.Models.Configurations;
using SolimusBlog.Application.ServiceCollection;
using SolimusBlog.Infrastructure.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));
builder.Services.Configure<DbConnectionsConfiguration>(builder.Configuration.GetSection("ConnectionStrings"));

// Получаем строку подключения из переменной окружения либо назначаем самостоятельно
var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? 
                               builder.Configuration.GetConnectionString("PostgresConnection");
var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ??
                            builder.Configuration.GetConnectionString("RedisConnection");

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddOpenApi();
builder.Services.AddOpenApiExtension();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Solimus Blog API");
        options.WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();
app.MapAllEndpoints();
app.Run();