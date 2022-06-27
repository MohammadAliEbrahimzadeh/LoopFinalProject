using LoopMainProject.DataAccess.Base;
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

        private CommentRepository? _commentRepository;

        private ReplyRepository? _replyRepository;
    }

    public partial class UnitOfWork
    {
        public UserRepository UserRepository =>
    _userRepository ??= new UserRepository(_context);


        public PostRepository PostRepository =>
    _postRepository ??= new PostRepository(_context);

        public CommentRepository CommentRepository =>
_commentRepository ??= new CommentRepository(_context);

        public ReplyRepository ReplyRepository =>
_replyRepository ??= new ReplyRepository(_context);

    }

    public partial class UnitOfWork
    {

        public int Commit() =>
    _context.SaveChanges();

        public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}
