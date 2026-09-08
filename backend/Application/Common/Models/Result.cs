namespace Application.Common.Models
{
    public class Result
    {
        private Result(bool isSuccess, IEnumerable<string> errors) =>
            (IsSuccess, Errors) = (isSuccess, errors);
        public bool IsSuccess { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public static Result Success() => new Result(true, Array.Empty<string>());
        public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);
    }
}