namespace SolimusBlog.Application.Models.DTO_s.Request.Authentication;

public record RegistrationRequest(string Email, string Password, string ConfirmPassword, string Username);