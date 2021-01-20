using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserAccountsApp.Core.Repositories.Base
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(long id);

        Task<int> CreateAsync(TEntity entity);
    }
}