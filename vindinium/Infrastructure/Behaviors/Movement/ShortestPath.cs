using System.Collections.Generic;
using System.Linq;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Movement
{
	public class DefaultMovement : IMovement
    {
		public Map Map;
        public DefaultMovement(Map board)
        {
            this.Map = board;
            this.Map.PopulateNodeParents();
            this.PopulateMovementCost();
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

        public List<Node> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var result = new List<Node>();
            var node = Map.NodeMap[closestChest.X, closestChest.Y];
            int depth;
            Node target = node;
            do
            {
                result.Add(target);
                depth = target.MovementCost;
                target = target.Parents.Where(n => n.Type != Tile.GOLD_MINE_1 && n.MovementCost > 0).OrderBy(n => n.MovementCost).First();
            }
            while (depth > 1); // 0 is the hero
            result = result.OrderBy(n => n.MovementCost).ToList();
            return result;
        }


	    private void PopulateMovementCost()
	    {
	        var hero = this.Map.NodeMap[this.GetHeroLocation().X, this.GetHeroLocation().Y];
	        int depth = 0;
	        hero.MovementCost = depth;
	        depth++;

            foreach (var parent in hero.Parents)
	        {
                AssignCost(depth, parent);
	            if (parent.Passable)
	            {
                    this.FindAllRoutes(depth, parent);
                }
	        }
	    }

	    private void FindAllRoutes(int depth, Node parentNode)
	    {
            depth++;
            foreach (var node in parentNode.Parents)
            {
                AssignCost(depth, node);
                this.FindAllRoutes(depth, node);
            }
        }

	    private void AssignCost(int cost, Node node)
	    {
            if (cost < node.MovementCost)
            {
                if (node.Type == Tile.IMPASSABLE_WOOD)
                {
                    node.Passable = false;
                    node.MovementCost = -1;
                }
                else if (node.Type == Tile.GOLD_MINE_1 || 
                    node.Type == Tile.GOLD_MINE_2 || 
                    node.Type == Tile.GOLD_MINE_3 || 
                    node.Type == Tile.GOLD_MINE_4 || 
                    node.Type == Tile.GOLD_MINE_NEUTRAL ||
                    node.Type == Tile.TAVERN ||
                    node.Type == Tile.HERO_2 ||
                    node.Type == Tile.HERO_3 ||
                    node.Type == Tile.HERO_4)
                {
                    node.MovementCost = cost;
                    node.Passable = false;
                }
                else
                {
                    node.MovementCost = cost;
                    node.Passable = true;
                }
            }
	    }


	}
}
