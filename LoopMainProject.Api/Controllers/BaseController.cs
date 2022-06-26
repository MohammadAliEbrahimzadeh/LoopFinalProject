using LoopMainProject.Api.Base;
using LoopMainProject.Business.Contract;
using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class BaseController<T> : ControllerBase, IBaseController<T> where T : BaseEntity
    {

        private readonly IBaseBusiness<T> _baseBusiness;

        public BaseController(IBaseBusiness<T> baseBusiness)
        {
            _baseBusiness = baseBusiness;
        }
    }
    public partial class BaseController<T>
    {

        [HttpPost]
        public async Task<SamanSalamatResponse?> CreateAsync(T t, CancellationToken cancellationToken)
        {
            return await _baseBusiness.CreateAsync(t, cancellationToken);
        }


    }
}
