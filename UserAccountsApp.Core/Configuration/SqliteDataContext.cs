using SQLite;
using System.Threading.Tasks;
using UserAccountsApp.Core.Entities;

namespace UserAccountsApp.Core.Configuration
{
    // sqlite docs: https://github.com/praeclarum/sqlite-net/wiki/Getting-Started
    public sealed class SqliteDataContext : IDataContext
    {
        private SQLiteAsyncConnection _db;
        public SQLiteAsyncConnection Db => _db;

        public Task<bool> Initialization { get; private set; }

        public SqliteDataContext(IDbConfig config)
        {
            _db = new SQLiteAsyncConnection(config.ConnectionPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            // async initialization
            Initialization = InitializeAsync();
        }

        private async Task<bool> InitializeAsync()
        {
            // init tables (auto migrates entity chages)
            // read me: https://github.com/praeclarum/sqlite-net/wiki/Automatic-Migrations
            await _db.CreateTableAsync<User>();

            return true;
        }
    }
}