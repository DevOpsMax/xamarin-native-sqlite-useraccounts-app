namespace UserAccountsApp.Core.Models.Results
{
    public class ValueResult<TValue> : IResult, IValueResult<TValue>
    {
        public bool IsSuccess { get; set; }

        public string FailureMessage { get; set; }

        public TValue Value { get; set; }

        public static IValueResult<TValue> Success(TValue value)
        {
            return new ValueResult<TValue> { IsSuccess = true, Value = value };
        }

        public static IValueResult<TValue> Failure(string message)
        {
            return new ValueResult<TValue> { IsSuccess = false, FailureMessage = message };
        }
    }
}