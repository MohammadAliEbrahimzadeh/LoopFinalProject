using LoopMainProject.DataAccess.Context;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.DataAccess.Repositories;

namespace LoopMainProject.DataAccess
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly LoopMainProjectContext _context;

        public UnitOfWork(LoopMainProjectContext context)
        {
            _context = context;
        }

    }
    public partial class UnitOfWork
    {
        private UserRepository? _userRepository;

        private PostRepository? _postRepository;
    }

    public partial class UnitOfWork
    {
        public UserRepository UserRepository =>
    _userRepository ??= new UserRepository(_context);


        public PostRepository PostRepository =>
    _postRepository ??= new PostRepository(_context);

    }

    public partial class UnitOfWork
    {

        public int Commit() =>
    _context.SaveChanges();

        public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}
