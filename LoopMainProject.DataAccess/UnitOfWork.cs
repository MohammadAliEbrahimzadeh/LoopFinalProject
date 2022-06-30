using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.DataAccess.Repositories;
using Sieve.Services;

namespace LoopMainProject.DataAccess
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly LoopMainProjectContext _context;
        private readonly ISieveProcessor _processor;

        public UnitOfWork(LoopMainProjectContext context, ISieveProcessor processor)
        {
            _context = context;
            _processor = processor;
        }

    }
    public partial class UnitOfWork
    {
        private UserRepository? _userRepository;

        private PostRepository? _postRepository;

        private CommentRepository? _commentRepository;

        private ReplyRepository? _replyRepository;

        private VoteRepository? _voteRepository;
    }

    public partial class UnitOfWork
    {
        public UserRepository UserRepository =>
    _userRepository ??= new UserRepository(_context);


        public PostRepository PostRepository =>
    _postRepository ??= new PostRepository(_context, _processor);

        public CommentRepository CommentRepository =>
_commentRepository ??= new CommentRepository(_context);

        public ReplyRepository ReplyRepository =>
_replyRepository ??= new ReplyRepository(_context);

        public VoteRepository VoteRepository =>
_voteRepository ??= new VoteRepository(_context);

    }

    public partial class UnitOfWork
    {

        public int Commit() =>
    _context.SaveChanges();

        public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}
