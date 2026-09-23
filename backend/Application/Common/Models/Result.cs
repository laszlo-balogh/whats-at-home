namespace Application.Common.Models
{
    public class Result
    {
        private Result(bool isSuccess, IEnumerable<(string Field, string Message)> errors) =>
            (IsSuccess, Errors) = (isSuccess, errors);
        public bool IsSuccess { get; private set; }
        public IEnumerable<(string Field, string Message)> Errors { get; private set; }

        public static Result Success() => new Result(true, Array.Empty<(string, string)>());
        public static Result Failure(IEnumerable<(string, string)> errors) => new Result(false, errors);
    }
}