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
                IsSuccess=true,
                ChangedId = newPost.Id,
                Message = "New Post Has Been Created Successfully"
            };
        }


    }
}
