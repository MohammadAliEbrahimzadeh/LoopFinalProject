﻿using LoopMainProject.DataAccess.Base;
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
    }
}