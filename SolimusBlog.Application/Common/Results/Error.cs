namespace SolimusBlog.Application.Common.Results;

public sealed record ApplicationError(string Code, string Message)
{
    internal static ApplicationError None => new (ErrorTypeConstant.None, string.Empty);   
}
