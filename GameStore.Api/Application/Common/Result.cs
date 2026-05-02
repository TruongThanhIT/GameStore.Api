namespace GameStore.Api.Application.Common;

public class Result
{
    private readonly string? _error;

    private Result() { }

    private Result(string error)
    {
        IsSuccess = false;
        _error = error;
    }

    public bool IsSuccess { get; } = true;
    public bool IsFailure => !IsSuccess;

    public string Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error on a successful result");

    public static Result Success() => new();
    public static Result Failure(string error) => new(error);
}

public class Result<T>
{
    private readonly T? _value;
    private readonly string? _error;

    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
        _error = null;
    }

    private Result(string error)
    {
        IsSuccess = false;
        _error = error;
        _value = default;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value on a failed result");

    public string Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error on a successful result");

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(error);

    public Result<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        IsSuccess ? Result<TResult>.Success(mapper(_value!)) : Result<TResult>.Failure(_error!);

    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> binder) =>
        IsSuccess ? binder(_value!) : Result<TResult>.Failure(_error!);

    public T GetValueOrDefault(T defaultValue = default!) =>
        IsSuccess ? _value! : defaultValue;

    public void Match(Action<T> onSuccess, Action<string> onFailure)
    {
        if (IsSuccess)
            onSuccess(_value!);
        else
            onFailure(_error!);
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure) =>
        IsSuccess ? onSuccess(_value!) : onFailure(_error!);
}
