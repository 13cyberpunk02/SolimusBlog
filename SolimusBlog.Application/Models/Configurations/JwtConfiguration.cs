namespace SolimusBlog.Application.Models.Configurations;

public class JwtConfiguration
{
    public string SecretKey { get; init; } = string.Empty; 
    public string Issuer { get; init; } = string.Empty; 
    public string Audience { get; init; } = string.Empty; 
    public int ExpirationInMinutes { get; set; } 
    public int RefreshTokenExpirationInDays { get; set; }
}
    