using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameStore.Application.Models.Requests
{
    public class PlatformRequest
    {
        public Guid Id { get; set; }

        public string? Type { get; set; }
    }
}
