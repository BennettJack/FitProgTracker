using fpt_backend.Controllers;

namespace fpt_backend.DbRepositories;

//Change to an error class later, enum fine for now
public enum RepositoryResultStatus
{
    NotFound,
    Duplicate,
    Success,
    Error,
    ValidationError
}

public class RepositoryResult<T, E>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Entity { get; }
    public E? Error { get; }

    private RepositoryResult(bool isSuccess, T? entity, E? error)
    {
        IsSuccess = isSuccess;
        Entity = entity;
        Error = error;
    }
    
    public static RepositoryResult<T, E> Ok(T entity) => new (true, entity, default);
    public static RepositoryResult<T, E> Fail(E error) => new(false, default, error);
    public static RepositoryResult<T, E> NotFound(E error) => new(isSuccess: false, default, error);

    public void Deconstruct(out bool isSuccess, out T? entity, out E? error)
    {
        isSuccess = IsSuccess;
        entity = Entity;
        error = Error;
    }
}