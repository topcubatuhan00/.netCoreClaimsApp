namespace Core.Utilities.Result.Abstract
{
    public interface IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
