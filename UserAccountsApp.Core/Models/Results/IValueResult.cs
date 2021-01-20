namespace UserAccountsApp.Core.Models.Results
{
    public interface IValueResult<TValue> : IResult
    {
        TValue Value { get; set; }
    }
}