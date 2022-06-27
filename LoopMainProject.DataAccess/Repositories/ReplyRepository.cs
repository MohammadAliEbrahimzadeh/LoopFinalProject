using LoopMainProject.DataAccess.Base;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
