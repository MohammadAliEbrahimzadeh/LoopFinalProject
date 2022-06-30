using Sieve.Attributes;
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
        [Sieve(CanSort = true, CanFilter = true)]
        public long UpvotesCount { get; set; }

        [Sieve(CanSort = true,CanFilter =true)]
        public long DownvotesCount { get; set; }

        public string Text { get; set; }

        [MaxLength(250)]
        [Sieve(CanSort = true, CanFilter = true)]
        public string Title { get; set; }
    }

    //Relations
    public partial class Post
    {
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ApplicationUser")]
        [Sieve(CanSort = true, CanFilter = true)]
        public int UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
