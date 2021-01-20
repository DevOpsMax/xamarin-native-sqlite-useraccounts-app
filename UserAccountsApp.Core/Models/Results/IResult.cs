namespace UserAccountsApp.Core.Models.Results
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string FailureMessage { get; set; }
    }
}