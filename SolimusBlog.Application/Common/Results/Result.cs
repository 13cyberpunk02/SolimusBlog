namespace SolimusBlog.Application.Common.Results;

public class Result
{
    protected bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ApplicationError Error { get; }

    protected Result(bool success, ApplicationError error)
    {
        if ((success && error != ApplicationError.None) || (!success && error == ApplicationError.None))
        {
            throw new InvalidOperationException("Невозможно выполнить операцию");
        }
        IsSuccess = success;
        Error = error;
    }
    
    public static Result Success() => new(true, ApplicationError.None);
    public static ResultT<TValue> Success<TValue>(TValue value) => new(value, true, ApplicationError.None);
    public static ResultT<TValue> Failure<TValue>(TValue value, ApplicationError error) => new(value, false, error);
    public static Result Failure(ApplicationError error) => new(false, error);
}