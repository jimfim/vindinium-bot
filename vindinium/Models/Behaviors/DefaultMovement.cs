using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public DefaultMovement(Map board)
        {
            this.Map = board;
            this.Map.PopulateNodeParents();
            PopulateMovementCost();
        }

	    public CoOrdinates GetHeroLocation()
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
		
		public Node GetClosestChest()
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
				viableChest.CalculateMovementCost(Map.HeroCurrentLocation.X, Map.HeroCurrentLocation.Y);
			}
			var closest = viableChests.OrderBy(c => c.MovementCost).First();
			return closest;
		}

        public List<Node> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var result = new List<Node>();
            var node = Map.NodeMap[closestChest.X, closestChest.Y];
            int depth = node.MovementCost;
            Node target = node;
            
            while (depth > 1) // 0 is the hero
            {
                result.Add(target);
    
                depth = target.MovementCost;
                target = target.Parents.Where(n => n.Type != Tile.GOLD_MINE_1).OrderBy(n => n.MovementCost).First();
                if (result.Any(n => n.Id == target.Id))
                {
                    break;
                }
            }
            if (!result.Any())
            {
                result.Add(target);
            }
            result.Reverse();
            return result;
        }


	    private void PopulateMovementCost()
	    {
	        var hero = Map.NodeMap[GetHeroLocation().X, GetHeroLocation().Y];
	        int depth = 0;
	        hero.MovementCost = depth;
	        depth++;

            foreach (var parent in hero.Parents)
	        {
	            if (parent.Passable)
	            {
	                parent.MovementCost = depth;
                    FindAllRoutes(depth, parent);
	            }
	        }
	    }

	    private void FindAllRoutes(int depth, Node parentNode)
	    {
            depth++;
            foreach (var node in parentNode.Parents)
            {
                if (node.Passable && depth < node.MovementCost)
                {
                    node.MovementCost = depth;
                    FindAllRoutes(depth, node);
                }                
            }
        }


	}
}
