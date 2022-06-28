using Sieve.Attributes;

namespace LoopMainProject.Model.Entities
{
    public partial class ApplicationUser : BaseEntity
    {

        [Sieve(CanFilter = true, CanSort = true)]
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    //Relations
    public partial class ApplicationUser
    {
        [Sieve(CanSort = true, CanFilter = true)]
        public ICollection<Post> Posts { get; set; }


        [Sieve(CanSort = true, CanFilter = true)]
        public ICollection<Comment> Comments { get; set; }


        [Sieve(CanSort = true, CanFilter = true)]
        public ICollection<Reply> Replies { get; set; }


        public ICollection<Vote> Votes { get; set; }
    }
}