using LoopMainProject.Business.Contract;
using LoopMainProject.Common.Helpers;
using LoopMainProject.Common.ViewModels;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Shop.Application.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Base
{
    public partial class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
    public partial class UserService
    {

        public async Task<bool> IsUsernameExists(string userName, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.UserNameExists(userName, cancellationToken);
        }

        public async Task<SamanSalamatResponse?> CreateUser(CreateUserViewModel UserVM, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.UserRepository.UserNameExists(UserVM.UserName, cancellationToken))
            {

                var newUser = new ApplicationUser()
                {
                    Username = UserVM.UserName,
                    Password = HashGenerator.GeneratePassword(UserVM.Password)
                    //Password = await PasswordHelper.GetHashStringAsync(UserVM.Password)
                };

                await _unitOfWork.UserRepository.CreateAsync(newUser, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    ChangedId = newUser.Id,
                    Message = "User Has Been Created"
                };
            }

            return new SamanSalamatResponse()
            {
                IsSuccess = false,
                Message = "Invalid UserName"
            };
        }

        public async Task<SamanSalamatResponse?> UpdateUser(string id, UpdateUserViewModel userVM, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.LoadByIdAsync(Int32.Parse(id), cancellationToken);

            if (user == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "Invalid Id",
                };
            }
            user.Password = await PasswordHelper.GetHashStringAsync(userVM.Password);
            user.LastUpdated = DateTime.Now;

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Has Been Updated",
            };
        }

        public async Task<bool> LoginUser(LoginUserViewModel userVM, HttpContext httpContext, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsername(userVM.Username, cancellationToken);

            if (user != null)
            {
                //if (HashGenerator.CheckPassword(user.Password, userVM.Password))
                //{
                //    var claims = new List<Claim>
                //        {
                //            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                //            new("UserName",user.Username),
                //        };

                //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //    var principal = new ClaimsPrincipal(identity);

                //    var properties = new AuthenticationProperties
                //    {
                //        IsPersistent = userVM.RememberMe
                //    };


                //    await httpContext.SignInAsync(principal, properties);
                //    return true;
                //}


                if (user.Password == await PasswordHelper.GetHashStringAsync(userVM.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                        new("UserName",user.Username),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = userVM.RememberMe
                    };


                    await httpContext.SignInAsync(principal, properties);
                    return true;

                }
                return false;
            }

            return false;

        }

        public async Task LogOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}

