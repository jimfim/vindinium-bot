using System;
using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Map
{
	public class MapNode : IMapNode
	{
		public int Id { get; set; }
		
		public int MovementCost { get; set; }

		public Tile Type { get; set; }

		public bool Passable { get; set; }

	    public List<IMapNode> Parents { get; set; }

		public CoOrdinates Location { get; }

		public MapNode(Tile type, int x, int y)
		{
			this.Type = type;
			this.Passable = false;
			this.Location = new CoOrdinates(x, y);
            this.MovementCost = Int32.MaxValue;
        }
	}
}
