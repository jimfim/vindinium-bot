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
		public int H { get; set; }

		public int G { get; set; }

		public Tile Type { get; set; }

		public bool Passable { get; set; }

		public List<Node> Parents { get; set; }

		public CoOrdinates location { get; set; }

		public int CalculateH(int x, int y)
		{
			H = (Math.Abs(this.location.X - x) + Math.Abs(this.location.Y - y));
			return this.H;
		}

		public int CalculateG()
		{
			foreach (var node in Parents)
			{
				node.G = node.H + 10;
			}
			return H;
		}

		public Node(Tile type, int x, int y)
		{
			Type = type;
			Passable = false;
			location = new CoOrdinates(x, y);
		}
	}
}
