namespace fpt_backend.Controllers;

public class Result<T>
{
    public bool IsSuccess => Status == ResultStatus.Success;
    public ResultStatus Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    
    public static Result<T> Ok(T data) => new (){ Status = ResultStatus.Success, Data = data };
    public static Result<T> NotFound(string message) => new (){ Status = ResultStatus.NotFound, Message = message };
    public static Result<T> Fail(string message) => new () { Status = ResultStatus.BadRequest, Message = message };
    public static Result<T> ErrorResult(string message) => new () { Status = ResultStatus.Error, Message = message };
}