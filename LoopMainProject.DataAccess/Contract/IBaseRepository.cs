using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace LoopMainProject.DataAccess.Contract
{
    public partial interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T t, CancellationToken cancellationToken);

        Task<T> UpdateAsync(T t, CancellationToken cancellationToken = new());

        Task<T> GetEntityById(int id, CancellationToken cancellationToken);

    }
}
