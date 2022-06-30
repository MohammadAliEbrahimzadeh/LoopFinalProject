using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoopMainProject.DataAccess.Repositories
{
    public partial class ReplyRepository : BaseRepository<Reply>
    {
        private readonly LoopMainProjectContext _context;

        public ReplyRepository(LoopMainProjectContext context) : base(context)
        {
            _context = context;
        }
    }
    public partial class ReplyRepository
    {
        public async Task<bool> HasVotedForReplyBefore(int userId, int replyId, CancellationToken cancellationToken)
        {
            return await _context.Votes.AnyAsync(v => v.ApplicationUserId == userId && v.ReplyId == replyId, cancellationToken);
        }
    }
}
