using System.Threading.Tasks;
using UserAccountsApp.Core.Configuration;
using UserAccountsApp.Core.Entities;
using UserAccountsApp.Core.Repositories.Base;

namespace UserAccountsApp.Core.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IDataContext _context;

        public UserRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> TryFindUserByUsername(string username)
        {
            var userCount = await _context.Db.Table<User>().CountAsync(x => x.Username == username);

            return userCount != 0;
        }

        public async Task<User> TryFindUserByCredentials(string username, string password)
        {
            return await _context.Db.Table<User>().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}