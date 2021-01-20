using System.Collections.Generic;
using System.Threading.Tasks;
using UserAccountsApp.Core.Configuration;
using UserAccountsApp.Core.Entities.Base;

namespace UserAccountsApp.Core.Repositories.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
    {
        private readonly IDataContext _context;

        public BaseRepository(IDataContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            // todo: refactor to use IQueryable for better filtering/sorting/querying
            return await _context.Db.Table<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _context.Db.FindAsync<TEntity>(id);
        }

        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            return await _context.Db.InsertAsync(entity);
        }
    }
}