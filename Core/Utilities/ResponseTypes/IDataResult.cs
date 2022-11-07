namespace Core.Utilities.ResponseTypes;

public interface IDataResult<T> : IResult
{
    T Data { get; set; }
}