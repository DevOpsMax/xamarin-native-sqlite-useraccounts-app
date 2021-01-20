using SQLite;
using UserAccountsApp.Core.Models.Async;

namespace UserAccountsApp.Core.Configuration
{
    public interface IDataContext : IAsyncInitialization
    {
        SQLiteAsyncConnection Db { get; }
    }
}