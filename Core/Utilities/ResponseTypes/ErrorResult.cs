namespace Core.Utilities.ResponseTypes;

public class ErrorResult : Result
{
    public ErrorResult(bool isSuccess, string message) : base(isSuccess, message)
    {
    }

    public ErrorResult(bool isSuccess) : base(isSuccess)
    {
    }
}