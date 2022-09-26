using OnlineGameStore.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Models.Views
{
    public class GameView
    {
        public Guid Key { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

    }
}
