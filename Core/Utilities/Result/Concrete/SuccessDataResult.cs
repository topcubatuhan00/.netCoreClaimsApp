namespace Core.Utilities.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, message, true)
        {
        }

        public SuccessDataResult(string message) : base(default, message, true)
        {
        }
    }
}
