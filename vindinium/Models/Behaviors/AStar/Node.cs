using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vindinium.Models.DTOs;

namespace vindinium.Models.Behaviors.AStar
{

	public class Node
	{
		public int Id { get; set; }
		// Movement Cost
		public int MovementCost { get; set; }

		public Tile Type { get; set; }

		public bool Passable { get; set; }

		public List<Node> Parents { get; set; }

		public CoOrdinates location { get; set; }

	    public int CalculateMovementCost(int x, int y)
		{
			MovementCost = (Math.Abs(this.location.X - x) + Math.Abs(this.location.Y - y));
			return this.MovementCost;
		}

		public Node(Tile type, int x, int y)
		{
			Type = type;
			Passable = false;
			location = new CoOrdinates(x, y);
            MovementCost = Int32.MaxValue;

        }
	}
}
