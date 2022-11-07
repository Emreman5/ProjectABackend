namespace Core.Utilities.ResponseTypes;

public class SuccessResult : Result
{

    public SuccessResult(string message) : base(true, message)
    {
    }

    public SuccessResult() : base(true)
    {
        
    }
}