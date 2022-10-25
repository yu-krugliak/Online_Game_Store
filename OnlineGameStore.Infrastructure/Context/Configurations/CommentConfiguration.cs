using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Infrastructure.Context.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(comment => comment.Id);

            builder.Property(comment => comment.Name)
                .IsRequired(true).HasMaxLength(60);

            builder.Property(comment => comment.Body)
                .IsRequired(true).HasMaxLength(500);

            builder.Property(comment => comment.DatePosted);

            builder.HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(post => post.UserIdCreated);

            builder.HasOne(comment => comment.ParentComment)
                .WithMany(parent => parent.CommentReplies)
                .HasForeignKey(comment => comment.ParentCommentId)
                .IsRequired(false);

            builder.HasMany(comment => comment.CommentReplies)
                .WithOne(reply => reply.ParentComment)
                .HasForeignKey(reply => reply.ParentCommentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasOne(comment => comment.Game)
                .WithMany(game => game.Comments)
                .HasForeignKey(comment => comment.GameId)
                .IsRequired(true);
        }
    }
}
