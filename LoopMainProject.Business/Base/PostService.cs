using LoopMainProject.Business.Contract;
using LoopMainProject.Common.ViewModels;
using LoopMainProject.DataAccess.Contract;
using LoopMainProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Business.Base
{
    public partial class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }

    public partial class PostService
    {
        public async Task<SamanSalamatResponse> CreatePost(string userId, CreatePostViewModel postVM, CancellationToken cancellationToken)
        {
            var newPost = new Post()
            {
                UserId = Int32.Parse(userId),
                Title = postVM.Title,
                Text = postVM.Text,
                DownvotesCount = 0,
                UpvotesCount = 0
            };

            await _unitOfWork.PostRepository.CreateAsync(newPost, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);


            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                ChangedId = newPost.Id,
                Message = "New Post Has Been Created Successfully"
            };
        }

        public async Task<SamanSalamatResponse> CreateComment(string userId, int postId, CreateCommentViewModel commentVM, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetEntityById(cancellationToken, postId);

            if (post == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "Post Wasnt Found"
                };
            }

            var newComment = new Comment()
            {
                UserId = Int32.Parse(userId),
                DownvotesCount = 0,
                UpvotesCount = 0,
                PostId = postId,
                Text = commentVM.Text,
            };



            await _unitOfWork.CommentRepository.CreateAsync(newComment, cancellationToken);

            await _unitOfWork.PostRepository.UpdateAsync(post, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "Commecnt Has Been Created"
            };
        }

        public async Task<SamanSalamatResponse> CreateReplay(string userId, int commentId, int? replyId, CreateCommentViewModel commentVM, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetEntityById(cancellationToken, commentId);

            if (comment == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "Comment Wasnt Found"
                };
            }

            var newReplay = new Reply()
            {
                UserId = Int32.Parse(userId),
                DownvotesCount = 0,
                UpvotesCount = 0,
                CommentId = commentId,
                Text = commentVM.Text,
            };

            var comments = await _unitOfWork.CommentRepository.GetPostByCommentId(commentId, cancellationToken);

            if (replyId != null && replyId != 0)
                newReplay.ParentId = replyId;


            await _unitOfWork.ReplyRepository.CreateAsync(newReplay, cancellationToken);

            await _unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);

            await _unitOfWork.PostRepository.UpdateAsync(comments.Post, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);


            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "Commecnt Has Been Created",
                ChangedId = newReplay.Id,
            };
        }

        public async Task<SamanSalamatResponse> CreateUpvotePost(string userId, int postId, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetEntityById(cancellationToken, postId);

            if (post == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Post Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.PostRepository.HasVotedForPostBefore(postId, Int32.Parse(userId), cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersPostVote(Int32.Parse(userId), postId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Upvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has Upvoted Before"
                    };

                post.DownvotesCount = post.DownvotesCount - 1;
                post.UpvotesCount = post.UpvotesCount + 1;
                await _unitOfWork.PostRepository.UpdateAsync(post, cancellationToken);

                newVote.VotesEnum = VotesEnum.Upvote;
                newVote.PostId = postId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Upvoted",
                    ChangedId = postId,
                };
            }


            post.UpvotesCount = post.UpvotesCount + 1;
            await _unitOfWork.PostRepository.UpdateAsync(post, cancellationToken);

            newVote.VotesEnum = VotesEnum.Upvote;
            newVote.PostId = postId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Upvoted",
                ChangedId = postId,
            };

        }

        public async Task<SamanSalamatResponse> CreateDownvotePost(string userId, int postId, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetEntityById(cancellationToken, postId);

            if (post == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Post Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.PostRepository.HasVotedForPostBefore(postId, Int32.Parse(userId), cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersPostVote(Int32.Parse(userId), postId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Downvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has Downvoted Before"
                    };

                post.DownvotesCount = post.DownvotesCount + 1;
                post.UpvotesCount = post.UpvotesCount - 1;
                await _unitOfWork.PostRepository.UpdateAsync(post, cancellationToken);

                newVote.VotesEnum = VotesEnum.Downvote;
                newVote.PostId = postId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Downvoted",
                    ChangedId = postId,
                };
            }


            post.UpvotesCount = post.DownvotesCount + 1;
            await _unitOfWork.PostRepository.UpdateAsync(post, cancellationToken);

            newVote.VotesEnum = VotesEnum.Downvote;
            newVote.PostId = postId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Upvoted",
                ChangedId = postId,
            };
        }


    }
}
