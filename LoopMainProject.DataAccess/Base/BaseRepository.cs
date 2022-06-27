using LoopMainProject.DataAccess.Context;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<T> GetEntityById(CancellationToken cancellationToken, int id) => (await _dbSet.SingleOrDefaultAsync(a => a.Id == id));

        public async Task<T> UpdateAsync(T t, CancellationToken cancellationToken = new())
        {
            t.LastUpdated = DateTime.Now;
            return (await Task.FromResult(_dbSet.Update(t))).Entity;
        }


    }
}
