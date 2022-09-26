using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Models.Views
{
    public class CommentView
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Body { get; set; }

        public DateTime? DatePosted { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Guid? GameId { get; set; }
    }
}
