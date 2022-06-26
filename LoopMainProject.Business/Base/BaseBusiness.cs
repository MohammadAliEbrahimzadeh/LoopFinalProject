using LoopMainProject.Business.Contract;
using LoopMainProject.Common.ViewModels;
using LoopMainProject.DataAccess.Context;
using LoopMainProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Base
{
    public partial class BaseBusiness<T> : IBaseBusiness<T> where T : BaseEntity
    {
     
    }
    public partial class BaseBusiness<T>
    {
        public Task<SamanSalamatResponse> CreateAsync(T t, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<SamanSalamatResponse> CreateUserAsync(ApplicationUser user, CancellationToken cancellationToken)
        //{

        //}
    }
}
