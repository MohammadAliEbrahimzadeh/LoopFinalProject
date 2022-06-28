using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Model.Entities
{
    public partial class Vote : BaseEntity
    {
        public VotesEnum VotesEnum { get; set; }
    }

    //Relations
    public partial class Vote
    {
        public Post Post { get; set; }
        public int? PostId { get; set; }

        public Comment Comment { get; set; }
        public int? CommentId { get; set; }

        public Reply Reply { get; set; }
        public int? ReplyId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public int ApplicationUserId { get; set; }
    }



    public enum VotesEnum
    {
        Upvote,
        Downvote
    }
}
