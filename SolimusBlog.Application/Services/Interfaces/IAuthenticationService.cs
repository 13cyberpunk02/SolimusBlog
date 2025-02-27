using SolimusBlog.Application.Common.Results;
using SolimusBlog.Application.Models.DTO_s.Request.Authentication;

namespace SolimusBlog.Application.Services.Interfaces;

public interface IAuthenticationService
{
    Task<Result> RegistrationAsync(RegistrationRequest registrationRequest);
    Task<Result> LoginAsync(LoginRequest loginRequest);
}