using System.ComponentModel.DataAnnotations.Schema;



namespace LoopMainProject.Model.Entities
{
    public partial class Reply : BaseEntity
    {
        public string Text { get; set; }

        public long UpvotesCount { get; set; }

        public long DownvotesCount { get; set; }
    }


    //Relations
    public partial class Reply
    {
        public Comment Comment { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public Reply Parent { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

    }
}
