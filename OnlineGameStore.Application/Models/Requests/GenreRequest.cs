using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Models.Requests
{
    public class GenreRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentGenreId { get; set; }
    }
}
