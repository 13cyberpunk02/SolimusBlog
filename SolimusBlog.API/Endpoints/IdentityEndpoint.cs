using SolimusBlog.API.Extensions;
using SolimusBlog.Application.Models.DTO_s.Request.Authentication;
using SolimusBlog.Application.Services.Interfaces;
using LoginRequest = SolimusBlog.Application.Models.DTO_s.Request.Authentication.LoginRequest;

namespace SolimusBlog.API.Endpoints;

public static class IdentityEndpoint
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("api/identity");
        group.MapPost("/login", Login);
        group.MapPost("/registration", Registration);
        return endpoints;
    }

    private static async Task<IResult> Login(LoginRequest loginRequest, IAuthenticationService authenticationService)
    {
        var response = await authenticationService.LoginAsync(loginRequest);
        return response.ToHttpResponse();
    }

    private static async Task<IResult> Registration(RegistrationRequest registerRequest,
        IAuthenticationService authenticationService)
    {
        var response = await authenticationService.RegistrationAsync(registerRequest);
        return response.ToHttpResponse();
    }
}