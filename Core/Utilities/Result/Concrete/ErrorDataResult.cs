﻿namespace Core.Utilities.Result.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, message, false)
        {
        }

        public ErrorDataResult(string message) : base(default, message, false)
        {
        }
    }
}
