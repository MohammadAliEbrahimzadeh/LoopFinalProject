using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
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
        private readonly ISieveProcessor _processor;

        public PostRepository(LoopMainProjectContext context, ISieveProcessor processor) : base(context)
        {
            _context = context;
            _processor = processor;
        }
    }
    public partial class PostRepository
    {
        public async Task<bool> HasVotedForPostBefore(int userId, int postId, CancellationToken cancellationToken)
        {
            return await _context.Votes.AnyAsync(v => v.ApplicationUserId == userId && v.PostId == postId, cancellationToken);
        }

        public async Task<List<Post>> LoadAllPostsAsync(SieveModel sieveModel,
         CancellationToken cancellationToken = new())
        {
            var query = _context.Posts.AsNoTracking().AsQueryable();
    
            return await _processor.Apply(sieveModel, query).ToListAsync(cancellationToken);
        }

        public async Task<List<Post>> LoadAllPostsByTitleAsync(SieveModel sieveModel,
      CancellationToken cancellationToken = new())
        {
            var query = _context.Posts.AsNoTracking().AsQueryable();
            return await _processor.Apply(sieveModel, query).ToListAsync(cancellationToken);
        }
    }
}
