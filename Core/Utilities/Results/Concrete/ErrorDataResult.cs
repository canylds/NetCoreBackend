﻿namespace Core.Utilities.Results.Concrete;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult() : base(false, default)
    {

    }

    public ErrorDataResult(string message) : base(false, message, default)
    {

    }

    public ErrorDataResult(T data) : base(false, data)
    {

    }

    public ErrorDataResult(string message, T data) : base(false, message, data)
    {

    }
}
