using System.Collections.Generic;
using System.Threading.Tasks;
using UserAccountsApp.Core.Models.Results;
using UserAccountsApp.Core.Entities;

namespace UserAccountsApp.Core.Services
{
    public interface IAccountService
    {
        Task<IResult> AttemptUserSignIn(string username, string password);
        Task<IResult> CreateUser(User user);
        Task<IValueResult<User>> GetUserById(long id);
        Task<IValueResult<List<User>>> GetUsers();
    }
}