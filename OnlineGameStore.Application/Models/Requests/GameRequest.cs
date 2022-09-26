using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Models.Requests
{
    public class GameRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

    }
}
