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
    public partial class PostRepository : BaseRepository<Post>
    {
        private readonly LoopMainProjectContext _context;

        public PostRepository(LoopMainProjectContext context) : base(context)
        {
            _context = context;
        }
    }
    public partial class PostRepository
    {
        public async Task<bool> HasVotedForPostBefore(int postId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Votes.AnyAsync(v => v.ApplicationUserId == userId && v.PostId == postId, cancellationToken);
        }
    
    }
}
