using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Model.Entities
{
    public partial class Post : BaseEntity
    {

        public long UpvotesCount { get; set; }

        public long DownvotesCount { get; set; }

        public string Text { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }
    }

    //Relations
    public partial class Post
    {
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
