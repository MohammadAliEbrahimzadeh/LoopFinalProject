using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;


namespace LoopMainProject.Business.Contract
{
    public interface IVotingService
    {
        Task<SamanSalamatResponse> CreateUpvotePost(string userId, int postId, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateDownvotePost(string userId, int postId, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateDownvoteReplay(string userId, int replyId, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateUpvoteReplay(string userId, int replyId, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateDownvoteComment(string userId, int commentId, CancellationToken cancellationToken);

        Task<SamanSalamatResponse> CreateUpvoteComment(string userId, int commentId, CancellationToken cancellationToken);
    }
}
