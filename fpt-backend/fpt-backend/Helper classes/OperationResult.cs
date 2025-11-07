namespace fpt_backend.Helper_classes;

//Change to an error class later, enum fine for now
public enum OperationStatus
{
    NotFound,
    Duplicate,
    Success,
    Error,
    ValidationError,
    BadRequest
}

public class OperationResult<T>
{
    public bool IsSuccess => Status == OperationStatus.Success;
    public OperationStatus Status { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }

    public static OperationResult<T> Success(T data) =>
        new() { Status = OperationStatus.Success, Data = data};

    public static OperationResult<T> Failure(string message, OperationStatus status = OperationStatus.Error) =>
        new() { Status = status, Message = message };
    
    public static OperationResult<T> NotFound(string message, OperationStatus status = OperationStatus.NotFound) =>
        new() { Status = status, Message = message };
}