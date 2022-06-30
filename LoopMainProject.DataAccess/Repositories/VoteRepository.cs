using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LoopMainProject.DataAccess.Repositories
{
    public partial class VoteRepository : BaseRepository<Vote>
    {

        private readonly LoopMainProjectContext _context;

        public VoteRepository(LoopMainProjectContext context) : base(context)
        {
            _context = context;
        }
    }

    public partial class VoteRepository
    {
        public async Task<Vote> GetLastUsersPostVote(int userId, int postId, CancellationToken cancellationToken)
        {
            return await _context.Votes.Where(a => a.ApplicationUserId == userId && a.PostId == postId).OrderByDescending(c => c.CreationDate).FirstOrDefaultAsync();
        }

        public async Task<Vote> GetLastUsersCommentVote(int userId, int commentId, CancellationToken cancellationToken)
        {
            return await _context.Votes.Where(a => a.ApplicationUserId == userId && a.CommentId == commentId).OrderByDescending(c => c.CreationDate).FirstOrDefaultAsync();
        }

        public async Task<Vote> GetLastUsersReplyVote(int userId, int replyId, CancellationToken cancellationToken)
        {
            return await _context.Votes.Where(a => a.ApplicationUserId == userId && a.ReplyId == replyId).OrderByDescending(c => c.CreationDate).FirstOrDefaultAsync();
        }
    }
}

