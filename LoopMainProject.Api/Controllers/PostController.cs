using LoopMainProject.Business.Contract;
using LoopMainProject.Common.Helpers;
using LoopMainProject.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class PostController
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContext;

        public PostController(IPostService postService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _httpContext = contextAccessor;
        }
    }

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
}
