using LoopMainProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Contract
{
    public interface IPostService
    {
        Task<SamanSalamatResponse> CreatePost(string userId, CreatePostViewModel postVM, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateComment(string userId, int postId, CreateCommentViewModel commentVM, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateReplay(string userId, int commentId, int? replyId, CreateCommentViewModel commentVM, CancellationToken cancellationToken);
    }
}
