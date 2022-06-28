using LoopMainProject.Model.Entities;
using Microsoft.EntityFrameworkCore;


namespace LoopMainProject.DataAccess.Context
{
    public partial class LoopMainProjectContext : DbContext
    {
        public LoopMainProjectContext(DbContextOptions<LoopMainProjectContext> options) : base(options)
        {

        }
    }

    public partial class LoopMainProjectContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Vote> Votes { get; set; }

    }

    public partial class LoopMainProjectContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(c => c.Replies)
               .WithOne(u => u.User)
               .HasForeignKey(fk => fk.UserId).OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region Post

            modelBuilder.Entity<Post>().ToTable("Posts");

            modelBuilder.Entity<Post>()
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(u => u.UserId);

            #endregion


            #region Coments

            modelBuilder.Entity<Comment>().ToTable("Comments");

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(fk => fk.PostId);

            #endregion


            #region Replies

            modelBuilder.Entity<Reply>().ToTable("Replies");

            modelBuilder.Entity<Reply>()
                .HasOne(p => p.Comment)
                .WithMany(c => c.Replies)
                .HasForeignKey(fk => fk.CommentId);


            #endregion


            #region Vote


            modelBuilder.Entity<Vote>().ToTable("Votes");


            modelBuilder.Entity<Vote>()
               .HasOne(c => c.Comment)
               .WithMany(u => u.Votes)
               .HasForeignKey(fk => fk.CommentId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Vote>()
               .HasOne(c => c.Reply)
               .WithMany(u => u.Votes)
               .HasForeignKey(fk => fk.ReplyId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Vote>()
               .HasOne(c => c.Post)
               .WithMany(u => u.Votes)
               .HasForeignKey(fk => fk.PostId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vote>()
              .HasOne(c => c.User)
              .WithMany(u => u.Votes)
              .HasForeignKey(fk => fk.ApplicationUserId).OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
