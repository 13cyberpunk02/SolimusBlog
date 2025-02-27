using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из переменной окружения либо назначаем самостоятельно
var postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ?? 
                               builder.Configuration.GetConnectionString("PostgresConnection");
var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ??
                            builder.Configuration.GetConnectionString("RedisConnection");


builder.Services.AddOpenApi();

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

app.Run();