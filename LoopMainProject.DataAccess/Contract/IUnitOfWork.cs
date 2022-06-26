using LoopMainProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.DataAccess.Contract
{
    public interface IUnitOfWork
    {
        UserRepository? UserRepository { get;}

        PostRepository? PostRepository { get; }

        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
