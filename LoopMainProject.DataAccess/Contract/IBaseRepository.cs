using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.DataAccess.Contract
{
    public partial interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T t, CancellationToken cancellationToken);

        Task<T> UpdateAsync(T t, CancellationToken cancellationToken = new());

        Task<T> GetEntityById(CancellationToken cancellationToken, int id);
    }
}
