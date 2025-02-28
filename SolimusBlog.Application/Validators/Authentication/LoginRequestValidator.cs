using FluentValidation;
using SolimusBlog.Application.Models.DTO_s.Request.Authentication;

namespace SolimusBlog.Application.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен к заполнению")
            .MinimumLength(6).WithMessage("Пароль должен состоять минимально от 6 символов")
            .MaximumLength(30).WithMessage("Максимальное количество символов в пароле 30");
    }
}