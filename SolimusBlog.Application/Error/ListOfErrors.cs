using SolimusBlog.Application.Common.Results;
namespace SolimusBlog.Application.Error;

public static class ListOfErrors
{
    public static ApplicationError CollectionOfErrors(IEnumerable<string> errors) => 
        new (ErrorTypeConstant.BadRequest, string.Join(Environment.NewLine, errors));
}