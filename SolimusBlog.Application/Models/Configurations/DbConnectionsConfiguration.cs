namespace SolimusBlog.Application.Models.Configurations;

public record DbConnectionsConfiguration(string PostgresConnection, string RedisConnection);