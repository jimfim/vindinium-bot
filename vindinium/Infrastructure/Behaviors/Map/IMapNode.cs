using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Map
{
    public interface IMapNode
    {
        List<IMapNode> Parents { get; set; }

        int Id { get; set; }

        int MovementCost { get; set; }

        CoOrdinates Location { get; }

        Tile Type { get; }

        bool Passable { get; set; }
    }
}