﻿using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Infrastructure.Entities
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }

        public DateTime? DatePosted { get; set; }

        public string? Body { get; set; }


        public Guid? OwnerId { get; set; }
        public virtual User? Owner { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual Comment? ParentComment { get; set; }

        public int? GameId { get; set; }
        public virtual Game? Game { get; set; }

        public virtual ICollection<Comment>? CommentReplies { get; set; }
    }
}
