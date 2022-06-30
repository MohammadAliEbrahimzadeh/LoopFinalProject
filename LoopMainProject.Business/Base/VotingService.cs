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
    public partial class VotingService : IVotingService
    {
        private readonly IUnitOfWork _unitOfWork;


        public VotingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }


    public partial class VotingService
    {
        public async Task<SamanSalamatResponse> CreateUpvotePost(string userId, int postId, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetEntityById(postId, cancellationToken);

            if (post == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Post Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.PostRepository.HasVotedForPostBefore(Int32.Parse(userId), postId, cancellationToken))
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
            var post = await _unitOfWork.PostRepository.GetEntityById(postId, cancellationToken);

            if (post == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Post Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.PostRepository.HasVotedForPostBefore(Int32.Parse(userId), postId, cancellationToken))
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

        public async Task<SamanSalamatResponse> CreateDownvoteComment(string userId, int commentId, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetEntityById(commentId, cancellationToken);

            if (comment == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No comment Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.CommentRepository.HasVotedForCommentBefore(Int32.Parse(userId), commentId, cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersCommentVote(Int32.Parse(userId), commentId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Downvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has Downvoted Before"
                    };

                comment.DownvotesCount = comment.DownvotesCount + 1;
                comment.UpvotesCount = comment.UpvotesCount - 1;
                await _unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);

                newVote.VotesEnum = VotesEnum.Downvote;
                newVote.CommentId = commentId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Downvoted",
                    ChangedId = commentId,
                };
            }


            comment.DownvotesCount = comment.DownvotesCount + 1;
            await _unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);

            newVote.VotesEnum = VotesEnum.Downvote;
            newVote.CommentId = commentId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Downvoted",
                ChangedId = commentId,
            };
        }

        public async Task<SamanSalamatResponse> CreateUpvoteComment(string userId, int commentId, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetEntityById(commentId, cancellationToken);

            if (comment == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No comment Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.CommentRepository.HasVotedForCommentBefore(Int32.Parse(userId), commentId, cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersCommentVote(Int32.Parse(userId), commentId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Upvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has upvoted Before"
                    };

                comment.DownvotesCount = comment.DownvotesCount + 1;
                comment.UpvotesCount = comment.UpvotesCount - 1;
                await _unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);

                newVote.VotesEnum = VotesEnum.Upvote;
                newVote.CommentId = commentId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Upvoted",
                    ChangedId = commentId,
                };
            }


            comment.UpvotesCount = comment.UpvotesCount + 1;
            await _unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);

            newVote.VotesEnum = VotesEnum.Upvote;
            newVote.CommentId = commentId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Upvoted",
                ChangedId = commentId,
            };
        }

        public async Task<SamanSalamatResponse> CreateUpvoteReplay(string userId, int replyId, CancellationToken cancellationToken)
        {
            var reply = await _unitOfWork.ReplyRepository.GetEntityById(replyId, cancellationToken);

            if (reply == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Reply Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.ReplyRepository.HasVotedForReplyBefore(Int32.Parse(userId), replyId, cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersReplyVote(Int32.Parse(userId), replyId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Upvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has Upvoted Before"
                    };

                reply.DownvotesCount = reply.DownvotesCount - 1;
                reply.UpvotesCount = reply.UpvotesCount + 1;
                await _unitOfWork.ReplyRepository.UpdateAsync(reply, cancellationToken);

                newVote.VotesEnum = VotesEnum.Upvote;
                newVote.ReplyId = replyId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Upvoted",
                    ChangedId = replyId,
                };
            }


            reply.UpvotesCount = reply.UpvotesCount + 1;
            await _unitOfWork.ReplyRepository.UpdateAsync(reply, cancellationToken);

            newVote.VotesEnum = VotesEnum.Upvote;
            newVote.ReplyId = replyId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Upvoted",
                ChangedId = replyId,
            };

        }

        public async Task<SamanSalamatResponse> CreateDownvoteReplay(string userId, int replyId, CancellationToken cancellationToken)
        {
            var reply = await _unitOfWork.ReplyRepository.GetEntityById(replyId, cancellationToken);

            if (reply == null)
            {
                return new SamanSalamatResponse()
                {
                    IsSuccess = false,
                    Message = "No Reply Was Found"
                };
            }

            var newVote = new Vote();

            if (await _unitOfWork.ReplyRepository.HasVotedForReplyBefore(Int32.Parse(userId), replyId, cancellationToken))
            {

                var votes = await _unitOfWork.VoteRepository.GetLastUsersReplyVote(Int32.Parse(userId), replyId, cancellationToken);

                if (votes != null && votes.VotesEnum == VotesEnum.Downvote)
                    return new SamanSalamatResponse()
                    {
                        IsSuccess = false,
                        Message = "User Has Downvoted Before"
                    };

                reply.DownvotesCount = reply.DownvotesCount + 1;
                reply.UpvotesCount = reply.UpvotesCount - 1;
                await _unitOfWork.ReplyRepository.UpdateAsync(reply, cancellationToken);

                newVote.VotesEnum = VotesEnum.Downvote;
                newVote.ReplyId = replyId;
                newVote.ApplicationUserId = Int32.Parse(userId);

                await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new SamanSalamatResponse()
                {
                    IsSuccess = true,
                    Message = "User Downvoted",
                    ChangedId = replyId,
                };
            }


            reply.UpvotesCount = reply.DownvotesCount + 1;
            await _unitOfWork.ReplyRepository.UpdateAsync(reply, cancellationToken);

            newVote.VotesEnum = VotesEnum.Downvote;
            newVote.ReplyId = replyId;
            newVote.ApplicationUserId = Int32.Parse(userId);

            await _unitOfWork.VoteRepository.CreateAsync(newVote, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SamanSalamatResponse()
            {
                IsSuccess = true,
                Message = "User Downvoted",
                ChangedId = replyId,
            };
        }
    }

 
}
