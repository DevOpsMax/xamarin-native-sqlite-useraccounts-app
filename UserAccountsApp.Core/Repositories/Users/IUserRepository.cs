using System.Threading.Tasks;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Base;

namespace UserAccountsApp.Core.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> TryFindUserByUsername(string username);
        Task<User> TryFindUserByCredentials(string username, string password);
    }
}