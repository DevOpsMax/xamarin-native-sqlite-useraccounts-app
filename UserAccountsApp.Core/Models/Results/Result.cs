namespace UserAccountsApp.Core.Models.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }

        public string FailureMessage { get; set; }

        public static IResult Success()
        {
            return new Result { IsSuccess = true };
        }

        public static IResult Failure(string message)
        {
            return new Result { IsSuccess = false, FailureMessage = message };
        }
    }
}