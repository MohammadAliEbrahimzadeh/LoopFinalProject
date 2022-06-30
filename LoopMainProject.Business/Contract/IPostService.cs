using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Contract
{
    //Creation
    public partial interface IPostService
    {
        Task<SamanSalamatResponse> CreatePost(string userId, CreatePostViewModel postVM, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateComment(string userId, int postId, CreateCommentViewModel commentVM, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateReplay(string userId, int commentId, int? replyId, CreateCommentViewModel commentVM, CancellationToken cancellationToken);
    }


    //Filtering
    public partial interface IPostService
    {
        Task<SamanSalamatResponse<List<Post>>?> SearchPosts(SieveModel model, string title, CancellationToken cancellationToken);
    }
}
