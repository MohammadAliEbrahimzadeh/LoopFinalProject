using LoopMainProject.DataAccess.Context;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;
using Sieve.Services;

namespace LoopMainProject.DataAccess.Base
{
    public partial class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public BaseRepository(LoopMainProjectContext loopMainProjectContext)
        {
            _dbSet = loopMainProjectContext.Set<T>();
        }

      
    }
    public partial class BaseRepository<T>
    {
        public async Task<T> CreateAsync(T t, CancellationToken cancellationToken) => (await _dbSet.AddAsync(t, cancellationToken)).Entity;

        public async Task<T> GetEntityById(int id, CancellationToken cancellationToken) => (await _dbSet.SingleOrDefaultAsync(a => a.Id == id));


        public async Task<T> UpdateAsync(T t, CancellationToken cancellationToken = new())
        {
            t.LastUpdated = DateTime.Now;
            return (await Task.FromResult(_dbSet.Update(t))).Entity;
        }

      
    }
}
