using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using vindinium.Models.Behaviors.AStar;
using vindinium.Models.DTOs;

namespace vindinium.Models.Behaviors
{
	public class DefaultMovement : IMovement
	{
		private readonly Tile[,] gameBoard;
		public Map Map;
		//public Node[,] NodeMap;

		public DefaultMovement(Tile[,] board)
		{
			this.gameBoard = board;
			this.Map = new Map(board);
			this.Map.CalculateParents();
			
		}

		

		public string ToClosestChest()
		{
			CoOrdinates currentLocation = null;
			for (int i = 0; i < this.Map.NodeMap.GetLength(0); i++)
			{
				for (int j = 0; j < this.Map.NodeMap.GetLength(1); j++)
				{
					if (this.Map.NodeMap[i, j].Type == Tile.HERO_1)
					{
						currentLocation = new CoOrdinates(i, j);
					}
				}
			}

			Node closestChest = this.GetClosestChest(currentLocation);
			Console.WriteLine("Moving to : {0} {1}", closestChest.location.X, closestChest.location.Y);
			this.Map.CalculateMovementCostFor(closestChest.location.X, closestChest.location.Y);
			return MoveToClosestChest(currentLocation);
		}

		private string MoveToClosestChest(CoOrdinates currentLocation)
		{
			var routes = this.Map.NodeMap[currentLocation.X, currentLocation.Y].Parents.Where(route => route.Passable).Reverse();
			var next = routes.First();

			string direction = "Stay";
			if (next.location.X > currentLocation.X)
			{
				direction= "East";
			}
			else if (next.location.X < currentLocation.X)
			{
				direction= "West";
			}
			else if (next.location.Y > currentLocation.Y)
			{
				direction= "South";
			}
			else if (next.location.Y < currentLocation.Y)
			{
				direction= "North";
			}
			Console.WriteLine(direction);
			return direction;
		}

		private Node GetClosestChest(CoOrdinates currentPosition)
		{
			var viableChests = new List<Node>();
			for (int i = 0; i < this.Map.NodeMap.GetLength(0); i++)
			{
				for (int j = 0; j < this.Map.NodeMap.GetLength(1); j++)
				{
					if (this.Map.NodeMap[i, j].Type == Tile.GOLD_MINE_NEUTRAL || this.Map.NodeMap[i, j].Type == Tile.GOLD_MINE_2 || this.Map.NodeMap[i, j].Type == Tile.GOLD_MINE_3 || this.Map.NodeMap[i, j].Type == Tile.GOLD_MINE_4)
					{
						viableChests.Add(this.Map.NodeMap[i,j]);
					}
				}
			}

			foreach (var viableChest in viableChests)
			{
				viableChest.CalculateH(currentPosition.X, currentPosition.Y);
			}
			var closest = viableChests.OrderBy(c => c.H).First();
			return closest;
		}
	}
}
