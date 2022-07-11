using LoopMainProject.Api.Controllers;
using LoopMainProject.Business.Contract;
using LoopMainProject.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Threading;
using FluentAssertions;


namespace LoopMainProject.Test.Unit
{
    public class UserControllerUnitTest
    {
        #region CreateUser

        [Fact]
        public void Create_User_Return_OkResult()
        {
            //Arange
            var userVM = new CreateUserViewModel()
            {
                UserName = "UserNameTest",
                Password = "PasswordTest",
            };

            SamanSalamatResponse response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateUser(userVM, cancellationToken)).ReturnsAsync(response);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = userController.CreateUserAsync(userVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(response.IsSuccess);

        }

        [Fact]
        public void Create_User_Return_InvalidUsername()
        {
            //Arange
            var userVM = new CreateUserViewModel()
            {
                UserName = "UserNameTest",
                Password = "PasswordTest",
            };

            SamanSalamatResponse response = new SamanSalamatResponse()
            {
                IsSuccess = false,
            };


            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateUser(userVM, cancellationToken)).ReturnsAsync(response);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = userController.CreateUserAsync(userVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(response.IsSuccess);
        }


        #endregion


        #region UpdateUser

        [Fact]
        public void Update_User_Return_OkResult()
        {
            //Arange
            var userVM = new UpdateUserViewModel()
            {
                Password = "PasswordTest",
            };

            SamanSalamatResponse response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext.User.Claims).Returns(context.User.FindAll(ClaimTypes.NameIdentifier));

            mockService.Setup(mockService => mockService.UpdateUser("1", userVM, cancellationToken)).ReturnsAsync(response);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = userController.UpdateUserAsync(userVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(response.IsSuccess);
        }

        [Fact]
        public void Update_User_Return_UserNotFound()
        {
            //Arange
            var userVM = new UpdateUserViewModel()
            {
                Password = "PasswordTest",
            };

            SamanSalamatResponse response = new SamanSalamatResponse()
            {
                IsSuccess = false,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext.User.Claims).Returns(context.User.FindAll(ClaimTypes.NameIdentifier));

            mockService.Setup(mockService => mockService.UpdateUser("1", userVM, cancellationToken)).ReturnsAsync(response);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = userController.UpdateUserAsync(userVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(response.IsSuccess);
        }

        #endregion


        #region Login

        [Fact]
        public void Login_User_Return_InvalidUsername()
        {
            //Arange
            var loginVM = new LoginUserViewModel()
            {
                Password = "afad",
                Username = "as",
                RememberMe = true
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.LoginUser(loginVM, context, cancellationToken)).ReturnsAsync(false);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);


            //Act
            var data = userController.Login(loginVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(false);

        }

        [Fact]
        public void Login_User_Return_OkResult()
        {
            //Arange
            var loginVM = new LoginUserViewModel()
            {
                Password = "afad",
                Username = "as",
                RememberMe = true
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IUserService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.LoginUser(loginVM, context, cancellationToken)).ReturnsAsync(true);

            var userController = new UserController(mockService.Object, mockHttpContext.Object);


            //Act
            var data = userController.Login(loginVM, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(true);

        }


        #endregion

    }
}
