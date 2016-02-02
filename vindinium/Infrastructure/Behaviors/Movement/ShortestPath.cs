using System.Collections.Generic;
using System.Linq;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Movement
{
	public class DefaultMovement : IMovement
    {
		private readonly IMapBuilder _defaultMapBuilderBuilder;

	    private Node HeroNode => _defaultMapBuilderBuilder.HeroNode;

	    public DefaultMovement(IMapBuilder board)
        {
            _defaultMapBuilderBuilder = board;
            PopulateMovementCost();
        }


        public List<Node> GetShortestCompleteRouteToLocation(CoOrdinates closestChest)
        {
            var result = new List<Node>();
            var node = _defaultMapBuilderBuilder.NodeMap[closestChest.X, closestChest.Y];
            int depth;
            Node target = node;
            do
            {
                result.Add(target);
                depth = target.MovementCost;
                target = target.Parents.Where(n => n.Type != Tile.GOLD_MINE_1 && n.MovementCost > 0).OrderBy(n => n.MovementCost).First();
                // no route to anything protection.. just wait
                if (result.Count > 100)
                {
                    return null;
                }
            }
            while (depth > 1); // 0 is the hero
            result = result.OrderBy(n => n.MovementCost).ToList();
            return result;
        }

	    private void PopulateMovementCost()
	    {
	        
	        int depth = 0;
	        HeroNode.MovementCost = depth;
	        depth++;

            foreach (var heroNode in HeroNode.Parents)
	        {
                AssignCost(depth, heroNode);
	            if (heroNode.Passable)
	            {
                    FindAllRoutes(depth, heroNode);
                }
	        }
	    }

	    private void FindAllRoutes(int depth, Node parentNode)
	    {
            depth++;
            foreach (var node in parentNode.Parents.Where(n => n.MovementCost > depth))
            {
                AssignCost(depth, node);
                if (node.Passable)
                {
                    FindAllRoutes(depth, node);
                }
            }
        }

	    private void AssignCost(int cost, Node node)
	    {
            if (cost < node.MovementCost)
            {
                if (node.Type == Tile.IMPASSABLE_WOOD || node.Type == Tile.GOLD_MINE_1)
                {
                    node.Passable = false;
                    node.MovementCost = -1;
                }
                else if (node.Type == Tile.GOLD_MINE_2 || 
                    node.Type == Tile.GOLD_MINE_3 || 
                    node.Type == Tile.GOLD_MINE_4 || 
                    node.Type == Tile.GOLD_MINE_NEUTRAL ||
                    node.Type == Tile.TAVERN ||
                    node.Type == Tile.HERO_1 ||
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
