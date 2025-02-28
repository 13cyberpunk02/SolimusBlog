using SolimusBlog.Application.Common.Results;

namespace SolimusBlog.Application.Error.AuthenticationErrors;

public static class AuthenticationError
{
    public static ApplicationError UserAlreadyExists => 
        new(ErrorTypeConstant.ValidationError, "Пользователь с данной эл. почтой уже зарегистрирован");
    
    public static ApplicationError UserEmailIsNotRegistered => 
        new(ErrorTypeConstant.NotFound, "Данная эл. почта не зарегистрирована");
}