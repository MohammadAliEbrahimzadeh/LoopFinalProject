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
    public class VotingControllerTest
    {
        [Fact]
        public void Create_Upvote_Comment_Return_OkPosts()
        {
            //Arange
            var response = new SamanSalamatResponse()
            {
                IsSuccess = true,
            };


            var mockHttpContext = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();

            var mockService = new Mock<IVotingService>();

            var cancellationToken = new CancellationToken();

            mockHttpContext.Setup(x => x.HttpContext).Returns(context);

            mockService.Setup(mockService => mockService.CreateUpvoteComment("1", 1, cancellationToken)).ReturnsAsync(response);

            var votingController = new VotingController(mockService.Object, mockHttpContext.Object);

            //Act
            var data = votingController.CreateUpvoteComment(1, cancellationToken).Result;

            //Assert
            data.IsSuccess.Should().Be(true);
        }
    }
}
