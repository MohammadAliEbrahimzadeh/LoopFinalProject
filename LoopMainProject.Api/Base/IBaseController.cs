using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Api.Base
{
    public interface IBaseController<T> where T : BaseEntity
    {
        Task<SamanSalamatResponse?> CreateAsync(T t, CancellationToken cancellationToken);
    }
}
