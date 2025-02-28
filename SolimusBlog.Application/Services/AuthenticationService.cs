using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SolimusBlog.Application.Common.Results;
using SolimusBlog.Application.Error;
using SolimusBlog.Application.Error.AuthenticationErrors;
using SolimusBlog.Application.Models.Configurations;
using SolimusBlog.Application.Models.DTO_s.Request.Authentication;
using SolimusBlog.Application.Models.DTO_s.Response.Authentication;
using SolimusBlog.Application.Services.Interfaces;
using SolimusBlog.Application.Validators.Authentication;
using SolimusBlog.Domain.Entities;

namespace SolimusBlog.Application.Services;

public class AuthenticationService(
    LoginRequestValidator loginRequestValidator,
    RegistrationRequestValidator registrationRequestValidator,
    IOptions<JwtConfiguration> jwtOptions,
    UserManager<SolimusUser> userManager,
    IJwtService jwtService) : IAuthenticationService
{
    public async Task<Result> RegistrationAsync(RegistrationRequest registrationRequest)
    {
        var validationResult = await registrationRequestValidator.ValidateAsync(registrationRequest);
        if (!validationResult.IsValid)
            return  Result.Failure(ListOfErrors.CollectionOfErrors(validationResult.Errors.Select(x => x.ErrorMessage)));
        
        var user = await userManager.FindByEmailAsync(registrationRequest.Email);
        if (user is not null)
            return Result.Failure(AuthenticationError.UserAlreadyExists);

        var newUser = new SolimusUser
        {
            Email = registrationRequest.Email,
            UserName = registrationRequest.Username,
            JoinedDate = DateTime.UtcNow
        };
        
        var result = await userManager.CreateAsync(newUser, registrationRequest.Password);
        if(!result.Succeeded)
            return Result.Failure(ListOfErrors.CollectionOfErrors(result.Errors.Select(x => x.Description)));
        
        return Result.Success("Регистрация прошла успешно, вы можете авторизоваться");
    }

    public async Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await loginRequestValidator.ValidateAsync(loginRequest);
        if(!validationResult.IsValid)
            return Result.Failure(ListOfErrors.CollectionOfErrors(validationResult.Errors.Select(x => x.ErrorMessage)));
        
        var user = await userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null)
            return Result.Failure(AuthenticationError.UserEmailIsNotRegistered);

        if (await userManager.IsLockedOutAsync(user))
        {
            var lockoutEndDate = user.LockoutEnd!.Value.ToLocalTime();
            var message = new[]
            {
                $"Ваш аккаунт {user.UserName} заблокирован из-за множества попыток входа неправильным паролем.",
                $"Повторно можно будет авторизоваться после {lockoutEndDate.Date:dd MMMM yyyy} время {lockoutEndDate.Hour}:{lockoutEndDate.Minute}"
            };
            return Result.Failure(ListOfErrors.CollectionOfErrors(message));
        }
        
        var isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isPasswordValid)
        {
            await userManager.AccessFailedAsync(user);
            var accessFailedCount = await userManager.GetAccessFailedCountAsync(user);
            var maxFailedAccessAttemptsBeforeLockout = 5;
            var attemptsLeft = maxFailedAccessAttemptsBeforeLockout - accessFailedCount;
            if (accessFailedCount != 0)
            {
                var message = new[]
                {
                    "Неверный пароль",
                    $"У вас осталось {attemptsLeft} {(attemptsLeft == 1 ? "попытка" : "попыток")} " +
                    "до блокировки вашего аккаунта"
                };
                return Result.Failure(ListOfErrors.CollectionOfErrors(message));
            }
            else
            {
                var lockOutEndDate = user.LockoutEnd!.Value.ToLocalTime();
                var message = new[]
                {
                    $"Ваш аккаунт {user.UserName} заблокирован из-за множества попыток входа неправильным паролем.",
                    $"Повторно можно будет авторизоваться после {lockOutEndDate.Date:dd MMMM yyyy} время {lockOutEndDate.Hour}:{lockOutEndDate.Minute}"
                };
                return Result.Failure(ListOfErrors.CollectionOfErrors(message));
            }
        }
        
        var roles = await userManager.GetRolesAsync(user);
        var accessToken = jwtService.GenerateAccessToken(user, roles.ToArray());
        var refreshToken = jwtService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays);
        var result = await userManager.UpdateAsync(user);
        
        if(!result.Succeeded)
            return Result.Failure(ListOfErrors.CollectionOfErrors(result.Errors.Select(x => x.Description)));
        return Result.Success(new LoginResponse(AccessToken: accessToken, RefreshToken: refreshToken));
    }
}