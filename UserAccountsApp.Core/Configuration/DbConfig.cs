using System.IO;
using Xamarin.Essentials;

namespace UserAccountsApp.Core.Configuration
{
    public class DbConfig : IDbConfig
    {
        private readonly string _connectionPath;

        public string ConnectionPath => _connectionPath;

        public DbConfig()
        {
            _connectionPath = Path.Combine(FileSystem.AppDataDirectory, "TestData.db");
        }

        public DbConfig(string overrideConnectionPath)
        {
            _connectionPath = overrideConnectionPath;
        }
    }
}