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
    public partial class UserRepository : BaseRepository<ApplicationUser>
    {
        private readonly LoopMainProjectContext _context;

        public UserRepository(LoopMainProjectContext context) : base(context)
        {
            _context = context;
        }
    }
    public partial class UserRepository
    {
        public async Task<bool> UserNameExists(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(a => a.Username == userName, cancellationToken);
        }

        public async Task<ApplicationUser> LoadByIdAsync(int id, CancellationToken cancellationToken = new()) =>
         (await _context.Users!
             .SingleOrDefaultAsync(x => x.Id == id, cancellationToken))!;


        public async Task<ApplicationUser> GetUserByUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username, cancellationToken);
        }
    }
}
