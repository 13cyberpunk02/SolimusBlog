namespace SolimusBlog.API.Configurations;

public record JwtConfiguration(
    string SecretKey, 
    string Issuer, 
    string Audience, 
    int ExpirationInMinutes, 
    int RefreshTokenExpirationInDays);