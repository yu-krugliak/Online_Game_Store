using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentView>> GetByGame(Guid gameKey);

        Task<CommentView> AddAsync(CommentRequest commentRequest);
    }
}
