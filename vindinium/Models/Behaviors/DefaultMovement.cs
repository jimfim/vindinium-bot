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
		public Map Map;
        public static Node blacklist;

        public DefaultMovement(Map board)
        {
            this.Map = board;
            this.Map.CalculateParents();

        }

	    public CoOrdinates HeroLocation()
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
	        return currentLocation;
	    }
		
		public string MoveToClosestChest(CoOrdinates currentLocation, CoOrdinates moveTo)
		{
            string direction = "Stay";
			if (moveTo.X > currentLocation.X)
			{
				direction= "East";
			}
			else if (moveTo.X < currentLocation.X)
			{
				direction= "West";
			}
			else if (moveTo.Y > currentLocation.Y)
			{
				direction= "South";
			}
			else if (moveTo.Y < currentLocation.Y)
			{
				direction= "North";
			}
			Console.WriteLine(direction);
			return direction;
		}

		public Node GetClosestChest(CoOrdinates currentPosition)
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

	    public List<Node> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
	    {
            List<Node> path = new List<Node>();
	        var target = Map.NodeMap[closestChest.X, closestChest.Y];
	        int depth = target.H;
            Node currentNode = target;
            path.Add(target);
	        while (depth != 0)
	        {
                var nextStep = currentNode.Parents.Where(tile => tile.Type == Tile.FREE || tile.Type == Tile.HERO_1).OrderBy(node => node.H).First();
	            path.Add(nextStep);
	            currentNode = nextStep;
	            depth = currentNode.H;
	        }
            return path;
	    }
	}
}
