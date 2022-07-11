using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.DataAccess.Repositories
{
    public partial class CommentRepository : BaseRepository<Comment>
    {
        private readonly LoopMainProjectContext _context;

        public CommentRepository(LoopMainProjectContext context) : base(context)
        {
            _context = context;
        }
    }
    public partial class CommentRepository
    {
        public async Task<Comment> GetPostByCommentId(int commentId, CancellationToken cancellationToken)
        {
            return await _context.Comments.Include(a => a.Post).SingleOrDefaultAsync(a => a.Id == commentId, cancellationToken);
        }

        public async Task<bool> HasVotedForCommentBefore(int userId, int commentId, CancellationToken cancellationToken)
        {
            return await _context.Votes.AnyAsync(v => v.ApplicationUserId == userId && v.CommentId == commentId, cancellationToken);
        }

        public async Task<List<Comment>> GetCommetsAndRepliesById(int id, CancellationToken cancellationToken)
        {
            return await _context.Comments.Include(r => r.Replies).Where(a => a.Id == id).ToListAsync();
        }
    }
}
