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
    public partial class PostController
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContext;
        private static Logger _logger = LogManager.GetLogger("LoopTestLogRules");

        public PostController(IPostService postService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _httpContext = contextAccessor;
        }
    }


    //Creation
    public partial class PostController
    {
        [HttpPost]
        [Route("CreatePost")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreatePost(CreatePostViewModel createPostViewModel, CancellationToken cancellationToken)
        {
            try
            {
                return await _postService.CreatePost(_httpContext.HttpContext.User.GetUserId(), createPostViewModel, cancellationToken);
            }
            catch (Exception)
            {
                //Todo Nlog
                throw;
            }

        }

        [HttpPost]
        [Route("CreateComment/{postId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateComment(CreateCommentViewModel createCommentVM, CancellationToken cancellationToken, int postId)
        {
            try
            {
                return await _postService.CreateComment(_httpContext.HttpContext.User.GetUserId(), postId, createCommentVM, cancellationToken);
            }
            catch (Exception)
            {
                //Todo Nlog
                throw;
            }

        }

        [HttpPost]
        [Route("CreateReplay/{commentId?}")]
        [Authorize]
        public async Task<SamanSalamatResponse> CreateReplay(CreateCommentViewModel createCommentVM, CancellationToken cancellationToken, int commentId, int replyId)
        {
            try
            {
                return await _postService.CreateReplay(_httpContext.HttpContext.User.GetUserId(), commentId, replyId, createCommentVM, cancellationToken);
            }
            catch (Exception)
            {
                //Todo Nlog
                throw;
            }

        }

    }



    //Filtering
    public partial class PostController
    {
        [HttpGet]
        [HttpHead]
        [Route("FilterPosts")]
        public async Task<SamanSalamatResponse<List<Post>>> FilterPosts([FromQuery] SieveModel sieveModel, string? title, CancellationToken cancellationToken)
        {
            return await _postService.SearchPosts(sieveModel, title, cancellationToken);
        }
    }

}
