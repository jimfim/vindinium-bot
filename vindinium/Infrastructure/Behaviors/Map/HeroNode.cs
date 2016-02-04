using System;
using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Map
{
    public class HeroNode : IMapNode
    {
        public string Name;
        //Score
        public int Elo;

        public int Life;

        public int Gold;

        public int MineCount;

        public bool Crashed;

        public List<IMapNode> Parents { get; set; }

        public int Id { get; set; }

        public int MovementCost { get; set; }

        public CoOrdinates Location { get; set; }

        public Tile Type { get; set; }

        public bool Passable { get; set; }

        public HeroNode(Tile type, int x, int y)
        {
            this.Type = type;
            this.Passable = false;
            this.Location = new CoOrdinates(x, y);
            this.MovementCost = Int32.MaxValue;
        }
    }
}
