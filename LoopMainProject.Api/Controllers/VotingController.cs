using LoopMainProject.Business.Contract;
using LoopMainProject.Common.Helpers;
using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Sieve.Models;

namespace LoopMainProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class VotingController
    {
        private readonly IVotingService _votingService;
        private readonly IHttpContextAccessor _httpContext;
        private static Logger _logger = LogManager.GetLogger("LoopTestLogRules");

        public VotingController(IVotingService votingService, IHttpContextAccessor contextAccessor)
        {
            _votingService = votingService;
            _httpContext = contextAccessor;
        }
    }

    public partial class VotingController
    {
        #region Voting

        [HttpPost]
        [Route("CreateUpvotePost/{postId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateUpvotePost(int postId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateUpvotePost(_httpContext.HttpContext.User.GetUserId(), postId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateUpvotePost));
                throw;
            }
        }

        [HttpPost]
        [Route("CreateDownvotePost/{postId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateDownvotePost(int postId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateDownvotePost(_httpContext.HttpContext.User.GetUserId(), postId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateDownvotePost));
                throw;
            }
        }


        [HttpPost]
        [Route("CreateUpvoteReply/{replyId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateUpvoteReply(int replyId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateUpvoteReplay(_httpContext.HttpContext.User.GetUserId(), replyId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateUpvoteReply));

                return new SamanSalamatResponse
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }


        [HttpPost]
        [Route("CreateDownvoteReply/{replyId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateDownvoteReply(int replyId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateDownvoteReplay(_httpContext.HttpContext.User.GetUserId(), replyId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateDownvoteReply));

                return new SamanSalamatResponse
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }


        [HttpPost]
        [Route("CreateUpvoteComment/{commentId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateUpvoteComment(int commentId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateUpvoteComment(_httpContext.HttpContext.User.GetUserId(), commentId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateUpvoteComment));

                return new SamanSalamatResponse
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }


        [HttpPost]
        [Route("CreateDownvoteComment/{commentId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateDownvoteComment(int commentId, CancellationToken cancellationToken)
        {
            try
            {
                return await _votingService.CreateDownvoteComment(_httpContext.HttpContext.User.GetUserId(), commentId, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("execption:" + e + "  controller:" + nameof(PostController) + "  action:" + nameof(VotingController.CreateDownvoteComment));

                return new SamanSalamatResponse
                {
                    IsSuccess = false,
                    Message = "Error",
                };
            }
        }

        #endregion
    }
}
