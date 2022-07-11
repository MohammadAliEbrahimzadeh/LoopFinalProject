using Sieve.Models;
using System;
using System.Collections.Generic;
using LoopMainProject.Model.Entities;
using LoopMainProject.Common.ViewModels;
using Moq;
using LoopMainProject.Business.Contract;
using Microsoft.AspNetCore.Http;
using System.Threading;
using LoopMainProject.Api.Controllers;
using FluentAssertions;

namespace LoopMainProject.Test.Unit
{
    public class PostControllerTest
    {

        #region Post

        [Fact]
        public void Filter_Post_Return_OkPosts()
        {
            //Arange
            var post = new Post()
            {
                UpvotesCount = 1,
                CreationDate = DateTime.Now,
                Title = "hi"
            };

            var post2 = new Post()
            {
                UpvotesCount = 2,
                CreationDate = DateTime.Now,
                Title = "bye"
            };

            var postList = new List<Post>();
            postList.Add(post);
            postList.Add(post2);

            var sieve = new SieveModel()
            {
                Filters = post.UpvotesCount.ToString(),
            };


            var response = new SamanSalamatResponse<List<Post>>()
            {
                IsSuccess = true,
                Data = postList
            };


            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IPostService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.SearchPosts(sieve, "test", cancellationToken)).ReturnsAsync(response);

            var postController = new PostController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = postController.FilterPosts(sieve, "test", cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(true);
        }


        [Fact]
        public void Create_Post_Return_OkResult()
        {
            //Arange

            var createVM = new CreatePostViewModel()
            {
                Text = "Test",
                Title = "TitleTest"
            };

            var response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IPostService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreatePost("1", createVM, cancellationToken)).ReturnsAsync(response);

            var postController = new PostController(mockService.Object, mockHttpContext.Object);

            //Act

            var data = postController.CreatePost(createVM, cancellationToken).Result;

            //Assert

            data.IsSuccess.Should().Be(true);

        }

        #endregion

        #region Comment

        [Fact]
        public void Create_Comment_Return_OkResult()
        {
            //Arange

            var createVM = new CreateCommentViewModel()
            {
                Text = "Test",
            };

            var response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IPostService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateComment("1", 1, createVM, cancellationToken)).ReturnsAsync(response);

            var postController = new PostController(mockService.Object, mockHttpContext.Object);

            //Act

            var data = postController.CreateComment(1, createVM, cancellationToken).Result;

            //Assert

            data.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Create_Comment_Return_Post_NotFound()
        {
            //Arange

            var createVM = new CreateCommentViewModel()
            {
                Text = "Test",
            };

            var response = new SamanSalamatResponse()
            {
                IsSuccess = false,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IPostService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateComment("1", 1, createVM, cancellationToken)).ReturnsAsync(response);

            var postController = new PostController(mockService.Object, mockHttpContext.Object);

            //Act

            var data = postController.CreateComment(1, createVM, cancellationToken).Result;

            //Assert

            data.IsSuccess.Should().Be(false);
        }

        #endregion

        #region Reply

        [Fact]
        public void Create_Reply_Return_Post_OkResult()
        {
            //Arange

            var createVM = new CreateCommentViewModel()
            {
                Text = "Test",
            };

            var response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };

            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IPostService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateReplay("1", 1, 1, createVM, cancellationToken)).ReturnsAsync(response);

            var postController = new PostController(mockService.Object, mockHttpContext.Object);

            //Act

            var data = postController.CreateReplay(1, 1, createVM, cancellationToken).Result;

            //Assert

            data.IsSuccess.Should().Be(true);
        }


        #endregion
    }
}
