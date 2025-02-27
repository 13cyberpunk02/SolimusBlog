namespace SolimusBlog.Application.Common.Results;

public class ResultT<TValue> : Result
{
    private readonly TValue _value;

    protected internal ResultT(TValue value, bool isSuccess, ApplicationError error) : base(isSuccess, error)
    {
        _value = value;
    }
    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Нет такой ошибки для назначения");
}