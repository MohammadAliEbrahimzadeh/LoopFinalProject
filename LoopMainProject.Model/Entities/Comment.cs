using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Model.Entities
{
    public partial class Comment : BaseEntity
    {
        public string Text { get; set; }

        public long UpvotesCount { get; set; }

        public long DownvotesCount { get; set; }

    }


    //Relations
    public partial class Comment
    {
        public Post Post { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
