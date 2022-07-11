using LoopMainProject.Business.Contract;
using LoopMainProject.Common.Helpers;
using LoopMainProject.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace LoopMainProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController
    {

        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContext;

        private static Logger _logger = LogManager.GetLogger("LoopTestLogRules");

        public UserController(IUserService userService, IHttpContextAccessor httpContext)
        {
            _userService = userService;
            _httpContext = httpContext;
        }

    }

    public partial class UserController
    {
    
        [HttpPost]
        [Route("CreateUserAsync")]
        public async Task<SamanSalamatResponse?> CreateUserAsync([FromBody] CreateUserViewModel user, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.CreateUser(user, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(UserController) + "  action:" + nameof(UserController.CreateUserAsync));
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateUserAsync")]
        [Authorize]
        public async Task<SamanSalamatResponse?> UpdateUserAsync(UpdateUserViewModel user, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.UpdateUser(_httpContext.HttpContext.User.GetUserId(), user, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(UserController) + "  action:" + nameof(UserController.UpdateUserAsync));
                throw;
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<SamanSalamatResponse> Login([FromBody] LoginUserViewModel loginVM, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.LoginUser(loginVM, _httpContext.HttpContext, cancellationToken);

                if (result)
                {
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = true,
                        Message = "Login Successful",

                    };
                }

                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "Login Not Successful",

                };
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(UserController) + "  action:" + nameof(UserController.Login));
                throw;
            }
        }


        [Route("LogOut")]
        [Authorize]
        [HttpGet]
        public async Task<SamanSalamatResponse> LogOut()
        {
            try
            {
                await _userService.LogOut(_httpContext.HttpContext);

                return new SamanSalamatResponse()
                {
                    Message = "Log Out Was Successful",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(UserController) + "  action:" + nameof(UserController.LogOut));
                throw;
            }

        }
    }
}
