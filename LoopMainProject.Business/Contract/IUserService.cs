using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Contract
{
    public interface IUserService
    {
        Task<SamanSalamatResponse?> CreateUser(CreateUserViewModel userVM, CancellationToken cancellationToken);

        Task<SamanSalamatResponse?> UpdateUser(int id, UpdateUserViewModel userVM, CancellationToken cancellationToken);

        Task<bool> IsUsernameExists(string userName, CancellationToken cancellationToken);

        Task<bool> LoginUser(LoginUserViewModel userVM, HttpContext httpContext, CancellationToken cancellationToken);

        Task LogOut(HttpContext httpContext);
    }
}
